AbstractBaseOperations
======================
| ``public class common.abstraction.AbstractBaseOperations``
|	This abstract class implements every method that is used by both the FileManager and Section classes, due to a similarity in functionality.	

:properties: * <string> OperationsTargetPath -  The target path to perform these operations on. This will act as the "root" of all operations.

---------

Constructors
~~~~~~~~~~~~
.. 

---------

.. code-block:: cs

	internal AbstractBaseOperations(string operationsTargetPath)
	
|	Main constructor for the AbstractBaseOperations class. Defines the operations target path.

:parameters:	* <string> operationsTargetPath - The target path to perform these operations on. This will act as the "root" of all operations.

---------

Method Implementations
~~~~~~~~~~~~~~~~~~~~~~
.. 

---------

.. code-block:: cs

	public void AddSection(string section)
	
| Adds a new section (Directory) into the file system.

:parameters:	* <string> section - The relative path of the section, relative to the root.

---------

.. code-block:: cs

	public void RemoveSection(string section)
	
| Removes a section (Directory) from the file system.

:parameters:	* <string> section - The relative path of the section, relative to the root.

---------

.. code-block:: cs

	public Section[] GetAllSections()
	
| Recursively searches for every section in the file system, and returns an array containing them.	

:returns: <Section[]> A Section[] containing the Section objects representing the directories.

---------

.. code-block:: cs

	public Section[] GetSectionsNamed(string name)
	
| Gets all the sections (Directories) in the file system, and returns the ones matching the specified name.

:parameters:	* <string> name - The name of the sections to search for.

:returns: <Section[]> A Section[] containing the objects representing each directory in the file system.

---------

.. code-block:: cs

	public Section GetFirstSectionNamed(string name)
	
| Gets all the sections (Directories) in the file system, and returns the first one with a matching name.

:parameters:	* <string> name - The name of the sections to search for.

:returns: <Section> A Section object representing the directory in the file system.

---------

.. code-block:: cs

	public Section GetSectionFromPath(string path)
	
| Returns a section based on the relative path provided, relative to the root.

:parameters:	* <string> path - The relative path to the section from the root

:returns: <Section> The Section object.

---------

.. code-block:: cs

	public void AddDocument(string documentName)
	
| Adds a document into the current Section.

:parameters:	* <string> documentName - The name of the document to add into the section

---------

.. code-block:: cs

	public void RemoveDocument(string documentName)
	
| Deletes a document from within a section, based on its relative path.

:parameters:	* <string> path - The name of the document to remove from the section

---------

.. code-block:: cs

	public string[] GetAllFiles()
	
| Iterates over every item stemming from the relative root used, filters out the files and returns an array with their full paths.

:returns: <string[]> A string[] containing every file stemming down from the root.

---------

.. code-block:: cs

	public string[] GetFilesNamed(string filename)
	
| Iterates over all the files stemming from the relative root and returns every name matched file.

:parameters:	* <string> filename - The filename to match with

:returns: <string[]> A string[] with all the files that matched the filename

---------

.. code-block:: cs

	public string GetFirstFileNamed(string filename)
	
| Iterates over all the files stemming from the relative root and returns the first name matched file.

:parameters:	* <string> filename - The filename to match with

:returns: <string> A string containing the full path of the first matched file
