feel free to make suggestions on my coding practices :)
```
// initialize an ini variable
public JSON_INI ini = new JSON_INI("example.ini");


// load settings
ini.readSettings();


// reading a value
// the second parameter on all of these methods is optional
// if the setting isn't found in the file, it will return the passed value (or default if not passed)

bool bValue = ini.readBool("bool_example", true);
long lValue = ini.readLong("long_example", 1337);
string sValue = ini.readString("string_example", "optional");
double dValue = ini.readDouble("double_example", 12.345);


// writing a value
// the second parameter takes an 'object' variable type

ini.writeItem("bool_example", bValue);
ini.writeItem("long_example", lValue);
ini.writeItem("string_example", sValue);
ini.writeItem("double_example", dValue);

// file is written as json format

{
  "bool_example": true,
  "long_example": 1337,
  "string_example": "optional",
  "double_example": 12.345
}
```
