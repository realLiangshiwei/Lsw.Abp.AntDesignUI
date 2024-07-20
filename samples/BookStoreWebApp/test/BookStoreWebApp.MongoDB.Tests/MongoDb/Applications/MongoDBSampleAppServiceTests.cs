using BookStoreWebApp.MongoDB;
using BookStoreWebApp.Samples;
using Xunit;

namespace BookStoreWebApp.MongoDb.Applications;

[Collection(BookStoreWebAppTestConsts.CollectionDefinitionName)]
public class MongoDBSampleAppServiceTests : SampleAppServiceTests<BookStoreWebAppMongoDbTestModule>
{

}
