FileUtils
=========
| ``public static utils.FileUtils``
| 	This class provides a wide variety of methods for quick and easy writing/reading from files.

---------

Method Implementations
~~~~~~~~~~~~~~~~~~~~~~
.. 


---------

.. code-block:: cs

	public static void EnsurePath(string path, FileAttributes pathType = FileAttributes.NotContentIndexed)
	
| Ensures the existence of the given path, be it a directory or a file.

:parameters:	* <string> path - The filepath to dump the data into.
				* <FileAttributes> pathType - Whether the path is a file or directory. If not specified, it will be automatically determined. This parameter accepts either a FileAttributes.Directory or FileAttributes.Normal type.
				
---------

.. code-block:: cs

	public static void DumpToFile(string path, List<string> data)
	
| Writes all the given data in bulk to the specified filepath.

:parameters:	* <string> path - The filepath to dump the data into.
				* <List<string>> data - A List containing the lines to write into the file.
				
---------

.. code-block:: cs

	public static void AppendToFile(string path, string data)
	
| Appends a given line to the end of the file.

:parameters:	* <string> path - The filepath to dump the data into.
				* <string> data - A string containing the line to write into the file.
				
---------

.. code-block:: cs

	public static List<string> ReadFromFile(string path)
	
| Reads all the lines from a file and returns them in the form of a list.

:parameters:	* <string> path - The filepath to dump the data into.
				
:returns: <List<string>> A list containing all the lines in the file

---------

.. code-block:: cs

	public static void DumpToFileBinary(string path, List<string> data)
	
| Writes all the given data in bulk, as binary information, into the specified filepath.

:parameters:	* <string> path - The filepath to dump the data into.
				* <List<string>> data - The primitive values to write into the file.

---------

.. code-block:: cs

	public static List<string> ReadFromFileBinary(string path)
	
| Reads the data in the specified filepath and returns it in the form of a list with 	all the values as strings.

:parameters:	* <string> path - The filepath to dump the data into.
:returns: <List<string>> The primitive values in a list of strings