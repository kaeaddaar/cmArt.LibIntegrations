# cmArt.LibIntegrations.MultUseOrig.sln

- This is a solution containing the shared code for multiple apps that connect to System Five by Windward Software.
- The biggest thing the code that connects to System Five does is assemble Inventory data into a usable entity and provide methods to calculate things like price schedules and quantities.
- IMPORTNAT: A migration to .Net6 has occurred search for cmArt.LibIntegrations.Tools for the .6 version of the code in the cmArt.LibIntegrations namespace. (some code may have been moved to move relevant places. ex: pricing calculations logic moved to the library that handles those calculations)
  - Link to demos including a couple examples of using the new .Net6 code: https://github.com/cmArt-Solutions/DemosAndPocs 
    - https://github.com/cmArt-Solutions/DemosAndPocs/tree/main/DemosAndPOCs/GetAssembledInventoryDataFromSystemFive
    - https://github.com/cmArt-Solutions/DemosAndPocs/tree/main/DemosAndPOCs/GetQueryDataFromPSQL
- A major challenge of library conflicts was introduced to the project as we started introducing .Net5 and .Net6 projects into the mix.

# cmArt.Shopify.App

- The Application originally build for Delta Water Products using pre .Net6 technology.
- This app creates a Windows Service that is designed to run silently on the database server. 
- The app performs ETL by Extracting raw table data from System Five; by performing multiple transformation steps (assemble data, perform calculations, split up into 3 target forms, transform to shopify specific forms); by pushing the data to an external Web Service that handles the complexities of sending the data to Shopify.

# cmArt.BevNet.App

- This application takes a BevNet price update file and builds a data load for System Five
- The app builds a data load file with just the records that need to be updated. 
- The cool thing about the tech in this app is that it pulls in data from the account records as well as assembling inventory entities to help build the data load file. This logic really made the functionality feel poloshed and complete.

# Shopify Theme
- The shopify theme of the delta site is a key part of the application as the theme code calls the External Web Service that provides the pricing base on price schedules to update the shopify UI. 

# Change Log
- 2022-11-01: Added Shopify Theme to source control
