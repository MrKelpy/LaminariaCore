﻿using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

// ReSharper disable InconsistentNaming

namespace LaminariaCore_General.utils
{
    /// <summary>
    /// This class contains a bunch of useful methods for interacting with the network
    /// </summary>
    public static class NetworkUtils
    {
        
        /// <summary>
        /// Returns the External IPv4 Address.
        /// </summary>
        /// <returns>A string containing the ip addr</returns>
        public static string GetExternalIPAddress() => new WebClient().DownloadString("https://checkip.amazonaws.com/").Trim();

        /// <summary>
        /// Returns the Local IPv4 Address.
        /// </summary>
        /// <returns>A string containing the ip addr</returns>
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?.ToString().Trim();
        }

        /// <summary>
        /// Determines the next available port based on a given starting port, up
        /// until port 65534. This is done by creating a TCP socket and trying to bind
        /// the port to the local IP Address.
        /// </summary>
        /// <param name="startingPort">The port to start the binding checks on</param>
        /// <returns>Either the starting port or the next available port after it</returns>
        public static int GetNextAvailablePort(int startingPort)
        {
            // Iterates through the ports until it finds one that's available, starting from the given port.
            for (int currentPort = startingPort; currentPort < IPEndPoint.MaxPort; currentPort++)
                if (!PortInUse(currentPort)) return currentPort;

            return -1;  // There are no more ports open.
        }
        
        /// <summary>
        /// Checks if a port is open or not, by accessing the list of active TCP listeners
        /// and checking if any endpoint has the same port as the one we're checking.
        /// </summary>
        /// <param name="port">The port to check</param>
        /// <returns>Whether the port is open or not</returns>
        private static bool PortInUse(int port)
        {
            IPEndPoint[] ipEndpoints = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners();
            return ipEndpoints.Any(endPoint => endPoint.Port == port);
        }
        
        /// <summary>
        /// Checks if the current machine has internet connectivity.
        /// </summary>
        /// <returns>True if it is, false if not, or an error occured.</returns>
        public static bool IsWifiConnected()
        {
            try
            {
                PingReply reply = new Ping().Send("google.com", 1000, new byte[32], new PingOptions());
                return reply != null && reply.Status == IPStatus.Success;
            }
            // There are many, many exceptions that can be thrown from a ping, so just catch them all.
            catch  { return false; }
        }
    }
}