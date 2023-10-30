PathUtils
=========
| ``public static utils.PathUtils``
| 	This class aims to provide a set of utilities to work with paths.

---------

Method Implementations
~~~~~~~~~~~~~~~~~~~~~~
.. 


---------

.. code-block:: cs

	public static string NormalizePath(string path)
	
| Normalizes a path, changing it to a common format. Every path should be formatted as such.: "path/to/directory"

:parameters:	* <string> path - The path to normalize
:returns: The normalized path

---------

.. code-block:: cs

	public static bool EqualsPath(this string path, string path2)
	
| Compares two paths, and returns true if they are equal.

:extension parameter:   <string> path - The target path, the one being called through the string
:parameters:    * <string> path2 - The target to compare the first path with
:returns: Whether the paths are equal or not
