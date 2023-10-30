PanelExtensions
===============
| ``public static forms.extensions.PanelExtensions``
|	This class is used to extend the functionality of the panels in Windows Forms.

---------

Method Implementations
~~~~~~~~~~~~~~~~~~~~~~
.. 

---------

.. note::
	AddAllFrom works by keeping track of the original parent forms of every control using an internal cache and 
	loading the components from and into them. Keep this in mind while using this method.
	
.. code-block:: cs

	public static void AddAllFrom(this Panel self, Panel targetPanel)
	
| This extension method is used to add the contents from an external panel into the current panel.

:extension parameter: 	<Panel> self - The panel the method is being called from.
:parameters:	* <Panel> targetPanel - The panel to add the contents of.

---------

.. note::
	SetAllFrom works in association with AddAllFrom, with the added effect that when used, every component in the panel will be sent back to their original parent.

.. code-block:: cs

	public static void SetAllFrom(this Panel self, Panel targetPanel)
	
| Replaces all the controls from the current panel with the controls from the target panel.

:extension parameter: 	<Panel> self - The panel the method is being called from.
:parameters:	* <Panel> targetPanel - The panel to clone the contents of.