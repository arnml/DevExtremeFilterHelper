# DevExtremeFilterHelper
Process loadOptions.Filter from DevExtreme DataGrid control to Filtering on the Server Side

The DevExtremeFilterHelper converts the [loadOptions.Filter]([https://openai.com](https://js.devexpress.com/jQuery/Documentation/ApiReference/Data_Layer/CustomStore/LoadOptions/#filter)) into a Dictionary where
* Keys are the Binary filter like "=", "<>", ">", ">=", "<", "<=", "startswith", "endswith", "contains", "notcontains"
* Values are Dictionaries where
  * Key: column name
  * Value: term to search in column with the Binary filter above

![image](https://github.com/arnml/DevExtremeFilterHelper/assets/169213539/11948131-47ec-4932-ba00-e1a808fdfddb)


Follow a sketch of the structure 
```
{
  "=": {
    col1: searchInCol1,
    col2: searchInCol2,
    ...
    coln: searchInColn
  },
  "contains": {
    col1: searchInCol1,
    col2: searchInCol2,
    ...
    coln: searchInColn
  },
  ...
  criteria: {
    col1: searchInCol1,
    col2: searchInCol2,
    ...
    coln: searchInColn
  }
}
```
