# lineInFile

CLI application that inspired by Ansible's `lineinfile` module, bringing it to the CLI of an operating system near you.

- it is idempotent - it will produce only the desired state you request, no matter how many times you run it. 
- its synxtax is simple and easy to read.

You can achieve similar results using `bash`, `grep`, `awk`, `sed` etc, but typically with a lot more required code.

## Install 

Download binary from [releases](https://github.com/shukriadams/lineinfile/releases), no dependencies required.
Place wherever executables live on your system, make executable with `chmod +x`.

## Use

Arguments

    --path | -p : The file to modify. Required.
    --line | -l : The line to insert/replace into the file. Required.
    --regex | -r : The regular expression to look for in every line of the file.
    --version | -v : Prints out the version of this tool.

### Examples

Ensure the environment variable `Foo` is created in .bashrc, and set to `Bar` in 

    lineinfile --path ~/.bashrc --line "export Foo=Bar"

Ensure the environment variable `Foo` is created in .bashrc, and if it already exists, is set to `Baz`.

    lineinfile --path ~/.bashrc --regex "export Foo=" --line "export Foo=Baz"

Note that when using both --regex and --line, the line value inserted into the file should match the regex. 
If this isn't the case, the line will be inserted each time the operation runs, as the regex will never
match it.

## License

GPL 3.0. See [license](https://github.com/shukriadams/lineinfile/blob/master/LICENSE) for details.
