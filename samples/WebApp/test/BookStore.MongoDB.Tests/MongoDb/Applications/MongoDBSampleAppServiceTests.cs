using BookStore.MongoDB;
using BookStore.Samples;
using Xunit;

namespace BookStore.MongoDb.Applications;

[Collection(BookStoreTestConsts.CollectionDefinitionName)]
public class MongoDBSampleAppServiceTests : SampleAppServiceTests<BookStoreMongoDbTestModule>
{

}
