import { Construct } from "constructs";
import { App, TerraformStack } from "cdktf";
import { AzurermProvider, ResourceGroup, CosmosdbAccount, CosmosdbSqlDatabase, CosmosdbSqlContainer } from "./.gen/providers/azurerm";

class Experiment extends TerraformStack {
  constructor(scope: Construct, name: string) {
    super(scope, name);
    const appName = "MyShortener"
    const location = "eastus";
    
    new AzurermProvider(this, "AzureRm", {
      features: {}
    })

    const rg = new ResourceGroup(this, "rg", {
      name: appName,
      location: location
    });

    const cosmosAccount = new CosmosdbAccount(this, `${appName}-cosmosdb-account`, {
      name: `${appName}-cosmosdb-account`.toLowerCase(),
      location: location,
      resourceGroupName: rg.name,
      offerType: "Standard",
      kind: "GlobalDocumentDB",

      consistencyPolicy: {
        consistencyLevel: "BoundedStaleness",
        maxIntervalInSeconds: 300,
        maxStalenessPrefix: 100000
      },

      capabilities: [
        {
          name: "EnableServerless"
        }
      ],

      geoLocation: [
        {
          location: location,
          failoverPriority: 0
        }
      ]

    });

    const cosmosDb = new CosmosdbSqlDatabase(this, `${appName}-cosmos-sql-db`, {
      name: `${appName}-cosmos-sql-db`,
      resourceGroupName: rg.name,
      accountName: cosmosAccount.name
    });

    new CosmosdbSqlContainer(this, `${appName}-cosmos-sql-db-container`, {
      name: `${appName}-cosmos-sql-db-container`,
      resourceGroupName: rg.name,
      accountName: cosmosAccount.name,
      databaseName: cosmosDb.name,
      partitionKeyPath: "/id"
    });

  }
}

const app = new App();
new Experiment(app, "deploy");
app.synth();
