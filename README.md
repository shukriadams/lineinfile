# lineInFile

CLI application that inserts a string into a file, or changes a part of a string in a file if the string already exists. 
Directly inspired by Ansible's `lineinfile` module.

- it is idempotent, ie, it will produce only the desired state you request, no matter how many times you run it. 
- its syntax is compact, simple and easy to read.

## Examples

Ensure environment variable `Foo` is created in .bashrc, and set to `Bar` in 

    lineinfile --path ~/.bashrc --line "export Foo=Bar"

Ensure environment variable `Foo` is created in .bashrc, and if it already exists, is set to `Baz`.

    lineinfile --path ~/.bashrc --regex "export Foo=" --line "export Foo=Baz"

Note that when using both --regex and --line, the line value inserted into the file should also match against the regex. 
If not, the line will be inserted each time the operation runs, as the regex will never match it.

## Arguments

    -p | --path    : The file to modify. Required.
    -l | --line    : The line to insert/replace into the file. Required.
    -r | --regex   : The regular expression to look for in every line of the file.
    -v | --version : Prints out the version of this tool.

## Install 

Download binary from [releases](https://github.com/shukriadams/lineinfile/releases), no dependencies required.
Place wherever executables live on your system, name it whatever you want (assuming `ìineInFile`), then make executable with `chmod +x`.

## This is dumb, why not use sed?

You can achieve similar results using `bash`, `grep`, and `sed`, but over several lines of code and
with conditional logic. It would likely look something like

    if grep -qxF -- "some_line" /file/to/path; then
        echo "text already in file"
    else
        sed -i 'i some_line' /file/to/path
    fi  

With `lineInFile` you can get the same result with 
    
    lineinfile -p /path/to/file -l 'some_line'
    
If you're scripting lots of files, that adds up.


## License

GPL 3.0. See [license](https://github.com/shukriadams/lineinfile/blob/master/LICENSE) for details.
