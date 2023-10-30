XMLUtils
=========
| ``public static utils.XMLUtils``
| 	This class offers a multitude of useful XML methods that make serializing, creating and reading XML easier.

---------

Method Implementations
~~~~~~~~~~~~~~~~~~~~~~
.. 

---------

.. code-block:: cs

	public static string SerializeToString<T>(object obj)
	
| Serializes an object into an XML string.

:parameters:	* <object> obj - The object to serialize.
:type parameters:	* T - A serializable class type
:returns: The serialized XML text
				
---------

.. code-block:: cs

	public static void SerializeToFile<T>(string filepath, object obj)
	
| Serializes an object into a given file automatically.

:parameters:	* <string> filepath - The filepath to add the text to
				* <object> obj - The object to serialize.
:type parameters:	* T - A serializable class type

---------

.. code-block:: cs

	public static T DeserializeFromFile<T>(string filepath)
	
| Deserializes an XML object from a file.

:parameters:	* <string> filepath  - The path to the file to be deserialized
:type parameters:	* T - The object type to deserialize the file contents into
:returns: An instance of T with the serialized information.

---------

.. code-block:: cs

	public static T DeserializeFromString<T>(string str)
	
| Deserializes the string into an object.

:parameters:	* <string> str - The string to deserialize
:type parameters:	* T - The type to deserialize the string into
:returns: An instance of T with the serialized information.