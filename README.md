# lineInFile

CLI application that brings Ansible's `lineinfile` module to an operating system near you. 

## Install 

Download binary from [releases](https://github.com/shukriadams/lineinfile/releases), no dependencies required.
Place wherever executables live on your system, make executable with `chmod +x`.

## Use

Arguments

    --path | -p : The file to modify. Required.
    --line | -l : The line to insert/replace into the file. Required.
    --regex | -r : The regular expression to look for in every line of the file.
    --version | -v : Prints out the version of this tool.

## License

GPL 3.0. See [license](https://github.com/shukriadams/lineinfile/blob/master/LICENSE) for details.
