FileManager
===========
| ``public class common.FileManager : AbstractBaseOperations``
|	This class implements an interface to interact with a program's file structure in a more convenient way.

.. note::

	This class is a child of AbstractBaseOperations, inheriting every method and property documented in it.
	
:properties: * <string> RootPath -  The root path of the file system.

---------

Constructors
~~~~~~~~~~~~
.. 

---------

.. code-block:: cs

	public FileManager(string root = null) : base(root)
	
|	Main constructor for the FileManager class. Sets the root path to the specified value. If not specified, 

:parameters:	* <string> operationsTargetPath - The target path to perform these operations on. This will act as the "root" of all operations.