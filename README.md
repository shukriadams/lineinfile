# lineInFile

Command line, based directly on Ansible's `lineinfile` module. 

## Install 

Download binary from [releases](https://github.com/shukriadams/lineinfile/releases), no dependencies required.
Place wherever executables live on your system, make executable with `chmod +x`.

## Use

Arguments

    --path | -p : The file to modify. Required.
    --line | -l : The line to insert/replace into the file. Required.
    --regex | -r : The regular expression to look for in every line of the file.
    --version | -v : Prints out the version of this tool.

