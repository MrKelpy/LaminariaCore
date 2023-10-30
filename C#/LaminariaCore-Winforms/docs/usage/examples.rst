Examples
========

Using SetAllFrom
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

.. code-block:: cs

	using PgpsUtilsAEFC.forms.extensions;
	(...)
	
	/// <summary>
	/// Sends the user to the Rating Form
	/// </summary>
	/// <param name="sender">The event sender</param>
	/// <param name="e">The event arguments</param>
	private void ButtonGoRating_Click(object sender, EventArgs e) =>
		this.MainLayout.SetAllFrom(ReviewForm.GetLayout());
		
---------

Using ReadFromFile and DumpToFile
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

.. code-block:: cs

	using PgpsUtilsAEFC.utils;
	(...)

	// Does the same, but for the file in memory
	List<string> data = FileUtils.ReadFromFile(ListingsPath);
	data.RemoveAll(x => x.Split(',')[0].ToLower().Equals(dialog.Name.ToLower()));
	data.Add($"{dialog.Name},{dialog.Price}");
	FileUtils.DumpToFile(ListingsPath, data);