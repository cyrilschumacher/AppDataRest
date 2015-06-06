# AppDataRest
## What's it?
**AppDataRest** is a library for retrieves a file (or even a list of files in a directory) contained the *Application Data* directory through a URL.

## Why this library?
I wanted to provide files to my website and to easily update mettres (without installing or spend a third party tool). With this library, I create a JSON file (or other type) and I moves in the Application Data directory. From there, I can call my file from a URL.

## How do I set it up?
The first step is add the line:

> AppDataRestRoutage.Register(config, "TO_REPLACE");

Congratulation! You haves set up the library in your ASP.NET project.

## Can I configure?
Of course! For configure the library, you must update your "Web.config" file in adding the next section:

> ```xml
> <appDataRestGroup>
>     <appDataRest>
>         <!-- Configuration here. -->
>     </appDataRest>
> </appDataRestGroup>
> ```

In the "appDataRest" tag, you can configure the library.

| Name            | Description                           | Value type         |
|:----------------|:--------------------------------------|:-------------------|
| **directory**   | For configure the directory display.  | *DirectoryElement* |
| **path**        | For configure the path                | *PathElement*      |

| Name                 |Property name        | Description                                                  | Value type             |
|:---------------------|:------------------- |:-------------------------------------------------------------|:-----------------------|
| **DirectoryElement** | Converters          | The Converters to convert the phonebook entries in a format. | *ConvertersCollection* |
|                      | DefaultDataFormat   | The default format for displaying directory entries.         | *string*               |
| **PathElement**      | Root                | The root directory name.                                     | *string*               |


## Example ##
For example, if I want obtain the "File.json" file since the "directory/sub/", I type the URL address:

> http://www.example.com/files/directory/sub/file/json

## Copyright

> The MIT License (MIT)
> 
> Copyright (c) 2015 Cyril Schumacher.fr
> 
> Permission is hereby granted, free of charge, to any person obtaining a copy
> of this software and associated documentation files (the "Software"), to deal
> in the Software without restriction, including without limitation the rights
> to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
> copies of the Software, and to permit persons to whom the Software is
> furnished to do so, subject to the following conditions:
> 
> The above copyright notice and this permission notice shall be included in all
> copies or substantial portions of the Software.
> 
> THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
> IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
> FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
> AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
> LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
> OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
> SOFTWARE.