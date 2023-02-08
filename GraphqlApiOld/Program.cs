using HotChocolate.AspNetCore;

namespace GraphqlApiOld
{
    public class Item
    {
        public string Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
    }

    internal class ItemObjectType : ObjectType<Item>
    {
        public const string ID_FIELD_NAME = "id";

        protected override void Configure(IObjectTypeDescriptor<Item> descriptor)
        {
            descriptor.Field(t => t.Id).Ignore();
            descriptor.Field(t => t.ExternalId).Name(ID_FIELD_NAME).Type<StringType>();
        }
    }

    public class Query
    {
        public static Item[] Items = 
        {
            new() {Id = "1", ExternalId = "Ext1", Name = "Cool item" },
            new() {Id = "2", ExternalId = "Ext2", Name = "Ok item"}
        };

        public IQueryable<Item> GetItems() => Items.AsQueryable();
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddType<ItemObjectType>()
                .AddFiltering()
                .AddSorting()
                .AddProjections();

            var app = builder.Build();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL().WithOptions( new GraphQLServerOptions()
                {
                    Tool =
                    {
                        Document = "{\r\n  items{\r\n    id\r\n    name\r\n  }\r\n}"
                    }
                });
            });

            app.Run();
        }
    }
}