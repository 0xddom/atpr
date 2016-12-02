# ATPR (Advanced Text Pattern Recognition)

[![Build Status](https://travis-ci.org/KuroAku/atpr.svg?branch=master)](https://travis-ci.org/KuroAku/atpr)
[![Build status](https://ci.appveyor.com/api/projects/status/64836q07f261hoti?svg=true)](https://ci.appveyor.com/project/KuroAku/atpr)

* alvaro.schuller@edu.uah.es
* carlos.cilleruelo@edu.uah.es
* danieldominguez05@gmail.com
* juanangel.lopezs@gmail.com

# What is ATPR?

# Install ATPR
Check the Wiki https://github.com/KuroAku/atpr/wiki for tutorials and installation steps. 

# Build
## *NIX
First install mono (and the IDE if you want)

You have to download Stanford libs and extract it in ~/Hackaton : [LIBS] https://www.dropbox.com/s/103t3z1tiiquizq/Standford.tar.gz?dl=0)


You can either load the .sln in Xamarin Studio/MonoDevelop or in a shell run:

    cd <the project path>
    nuget restore
    xbuild /p:Configuration=<Debug or Release> ATPR.sln
    
## Windows
Install Xamarin Studio and the c# toolchain

You have to download Stanford libs and extract it in ~/Hackaton : [LIBS] https://www.dropbox.com/s/103t3z1tiiquizq/Standford.tar.gz?dl=0)

Load the project in Xamarin Studio (prefered) or VS (not tested) and build, or in a shell:

    cd <project path>
    nuget restore
    msbuild ATPR.sln
    
# Usage
## Using ATPR with Autopsy
TODO

## Using ATPR with command line

#### Entities extractor:

    mono atpr -c 1 -p pathToDocuments [-o outputFile ]

If `-o` is missing the output will be redirected to STDOUT.

#### Dictionary generator:

    mono atpr -c 2 -p pathToDocuments [-o outputFile ]

If `-o` is missing the output will be redirected to STDOUT.

TODO: Complete usage

# Functions
ATPR is a tool focused in the analysis of documents using NLP(Natural Language Processing). 

The aplication support the following documents types:
* doc
* docx
* pdf

The main target for this tool is to help the forensics community in processing evidences with large amount of documents.
