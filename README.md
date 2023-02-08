# Graphql 12 to 13 schema break

Run application (default version is 12.6.2) there is sample query which is passing and returns
```
{
  "data": {
    "items": [
      {
        "id": "Ext1",
        "name": "Cool item"
      },
      {
        "id": "Ext2",
        "name": "Ok item"
      }
    ]
  }
}
```

Change version to 13.0.0 in csproj. Run application again. Same query now fails because there is missing 'id' field in schema and request is failing

```
{
  "errors": [
    {
      "message": "The field `id` does not exist on the type `Item`.",
      "locations": [
        {
          "line": 3,
          "column": 5
        }
      ],
      "path": [
        "items"
      ],
      "extensions": {
        "type": "Item",
        "field": "id",
        "responseName": "id",
        "specifiedBy": "http://spec.graphql.org/October2021/#sec-Field-Selections-on-Objects-Interfaces-and-Unions-Types"
      }
    }
  ]
}
```
