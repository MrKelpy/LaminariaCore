OriginalParentCache
===================
| ``public static internals.caches.OriginalParentCache``
| 	This singleton internal class implements a cache that stores every control's original parent, so that the dynamic loading/unloading of active components can be managed smoothly.

.. tip::
	This class cannot be used directly by the end user, save for with reflection. Just because you can access it with reflection, however, doesn't mean that you should. This is a highly sensitive class that may break your program when misused. 
	**This class is documented here only for maintainability purposes.**
	
:properties: * <OriginalParentCache> INSTANCE -  The instance used to access the class, since it is a singleton.
			 * Dictionary<string, Control> cache - The cache containing the original parents of every control.
	
---------

Constructors
~~~~~~~~~~~~
.. 

---------

.. code-block:: cs

	private OriginalParentCache()
	
|	This constructor is purely a singleton-enforcing ctor.

---------

Method Implementations
~~~~~~~~~~~~~~~~~~~~~~
.. 

---------

.. code-block:: cs

	internal bool AddToCache(Control control)
	
| Adds the specified control to the cache, in the form of a name:parent mapping.

:parameters:	* <Control> control - The control to add to the cache.
:returns: <bool> Whether or not the control was successfully added to cache.

---------

.. code-block:: cs

	internal Control GetParentOf(string name)
	
| Returns the parent of the specified control, as it was before it was added to the cache.

:parameters:	* <string> name - The name of the control to look for in the cache.
:returns: <Control> The cached original parent for the specified name.

---------

.. code-block:: cs

	internal bool Contains(string name)
	
| Checks if the specified control is cached.

:parameters:	* <string> name - The name of the control to look for in the cache.
:returns: <bool> Whether the control exists in the cache or not.