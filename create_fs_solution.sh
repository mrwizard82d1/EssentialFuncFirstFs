#!/bin/bash

# See 
# [this Stack Overflow post](https://stackoverflow.com/questions/16483119/an-example-of-how-to-use-getopts-in-bash)
# for the example of `getopts` which I use in this script.
#

usage() { echo "Usage: $0 [-s <solution_name>] [-p <project_name>] [-t <project_type>]" 1>&2; exit 1; }

SOLUTION_NAME=MySolution
PROJECT_NAME=MyProject
PROJECT_TYPE=console

while getopts ":h:s:p:t:" o; do
    case "${o}" in

        s)
            SOLUTION_NAME=${OPTARG}
            ;;
        p)
            PROJECT_NAME=${OPTARG}
            ;;
        t)
            PROJECT_TYPE=${OPTARG}
            ;;
        *)
            usage
            ;;
   esac
done
shift $((OPTIND-1))

if [ -z "${SOLUTION_NAME}" ] || [ -z "${PROJECT_NAME}" ] || [ -z "${PROJECT_TYPE}" ] ; then
    usage
fi

dotnet new sln -o ${SOLUTION_NAME}
cd ${SOLUTION_NAME}
mkdir src
dotnet new ${PROJECT_TYPE} -lang F# -o src/${PROJECT_NAME}
dotnet sln add src/${PROJECT_NAME}/${PROJECT_NAME}.fsproj
mkdir tests
dotnet new xunit -lang F# -o tests/${PROJECT_NAME}Tests
dotnet sln add tests/${PROJECT_NAME}Tests/${PROJECT_NAME}Tests.fsproj
cd tests/${PROJECT_NAME}Tests
dotnet add reference ../../src/${PROJECT_NAME}/${PROJECT_NAME}.fsproj
dotnet add package FsUnit
dotnet add package FsUnit.XUnit
dotnet build
dotnet test

