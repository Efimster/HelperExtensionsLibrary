The library of helper extensions useful in any project

* AttributesExtensions
* CollectionExtensions
* DictionaryExtensions
* IEnumerableExtensions
* ObjectExtensions
* ReflectionExtenbsions
* StringExtensions

AttributesExtensions:
	FilterPropertiesByAttribute() -  Filter properties by assigned attribute;

CollectionExtensions:
	KeyedCollection - Collection with items accessed by keys;
	StringKeyCollection - Collection with items accessed by string keys
	IsEmpty() - Checks whether collection equels to NULL or contains no items

DictionaryExtensions:
	TryGetValue() - Convenient version of TryGetValue method for dictionaries with value type key;
	ToDictionary() - Enumerable To Dictionary conversion;

IEnumerableExtensions:
	ForEach() - iterates over collection and aplies action on every item;
	Convert() - Converts one collection to another one by applying conversion function;
	JoinByIndex() - Merges two collections by applying selector function to elements with same index;
	EquelsByIndex() - Checks whether two itarative collections have identical items (include items count and places);
	ElementAtOrDefault() - Iterates over collection to element with given index. Returns element or given default value if the index is out of bounds ;
	FirstIndex() - Returns the first index of element in a sequence that satisfies a specified condition;
		
ObjectExtensions:
	ToPropertyValuesDictionary() - Converts Object to dictionary of it's properties
	GetDefaultValue() - Gets default value of the given type;
	DeepCopy() - Creates object deep copy using BinaryFormatter;
	DeepCopyByJSON() - Creates object deep copy using JSON;
	DeepCopyListByJSON() - Makes deep copy of list object using JSON;

ReflectionExtenbsions:
	DynamicPropertiesObject - Object with dynamically accessed properties;
	Nameof() - Returns delegate to return method name called by given delegate;
		+1   - Returns delegate to return property or field name, accessed by given delegate;
    Infoof() - Returns property information of method called by given delegate;
		+1   - Returns property information of property or field accessed by given delegate;
	ConstructFieldOrPropertyGetter() - Constructs Delegate to get field or property value;
	ConstructFieldOrPropertySetter() - Gets Action for field or property setting;

StringExtensions:
	SplitExt() - Splits string or string collection to enumerable collection and yields the results;
	Join2String() - Concatenates collection elemens to string using delimiter;
	IsEmpty() - string.IsNullOrEmpty(value) counterpart;
	SplitToDictionaryExt() - Splits string to dictionary;