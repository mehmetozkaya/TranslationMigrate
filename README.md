# TranslationMigrate
Move Translation records from Dev to MasterDev Environment.
Using Xrm assembly, connect dynamics organization service and compare translations.

/// Fetch last 5000 record from MasterDev
/// Get last 10 day record which last modified on from Dev
/// Compare Dev to MasterDev, if masterDev has no record, directly insert MasterDev db. 
/// If exist and not match directly update.
