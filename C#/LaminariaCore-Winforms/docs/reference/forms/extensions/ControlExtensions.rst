ControlExtensions
=================
| ``public static forms.extensions.ControlExtensions``
|  This class is used to extend the functionality of every control in Windows Forms.

---------

Method Implementations
~~~~~~~~~~~~~~~~~~~~~~
.. 

---------

.. code-block:: cs

	public static void SendToOriginal(this Control self)
	
| Searches for the original parent of the control inside OriginalParentCache, and sets the control's parent back to it.

:extension parameter: 	<Control> self - The control the method is being called from.

---------

.. code-block:: cs

	public static bool IsCached(this Control self)
	
| Checks whether the control is already contained in the cache.

:extension parameter: 	<Control> self - The control the method is being called from.

:returns: <bool> Whether the control is cached or not.

---------

.. warning::
	You're most likely doing something wrong if you're using this method. It shouldn't be used by you in 99% of the cases, but it's here for that 1%.
	If you're using this method, make sure you know what you're doing.
	
.. code-block:: cs

	public static void ForceCache(this Control self)
	
| Force the caching of the original parent of the control.

:extension parameter: 	<Control> self - The control the method is being called from.