Section
=======
| ``public class common.Section : AbstractBaseOperations``
|	This class represents a Section inside the File System provided by the FileManager class. It can't be instantiated by the user, and will only be accessible through the FileManager.

:properties: * <string> SectionFullPath -  The full path for the section.
			 * <string> Name - The section name.
			 
---------

Constructors
~~~~~~~~~~~~
.. 

---------

.. code-block:: cs

	internal Section(string sectionPath) : base(sectionPath)
	
|	Main constructor for the Section class. Takes in the root of the file system and the full path of the section.

:parameters:	* <string> sectionPath - The full path of the section.