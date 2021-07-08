# Authors
#	Aleksander Kovač
#
# This script will push all Release packages to nuget.org. Projects can be excluded with the following setting:
#  <PropertyGroup>
#    <IsPackable>false</IsPackable>
#  </PropertyGroup>
#
# Set the nuget api key first:
# nuget setApiKey <key> -Source https://api.nuget.org/v3/index.json
# nuget setApiKey <key> -Source https://www.nuget.org/api/v2/symbolpackage
# The key must be configured to permit this, first perform a manual upload of a package 
# and then edit the key by adding the new package as permitted.
#
# Default push source parameter also needs to be added to %AppData%/NuGet/NuGet.Config:
# <?xml version="1.0" encoding="utf-8"?>
# <configuration>
#    ...
#	<config>
#        <add key="defaultPushSource" value="https://api.nuget.org/v3/index.json" />
#	</config>
#	...
# </configuration>
#
# References
# https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-push
# https://docs.microsoft.com/en-us/nuget/reference/nuget-config-file

# Remove existing packages
find . -wholename '*/Release/*.nupkg' -execdir rm {} \;
find . -wholename '*/Release/*.snupkg' -execdir rm {} \;

# Create packages
dotnet clean
dotnet restore
dotnet test
if [ $? -ne 0 ]; then
	echo "Will not release packages because tests failed"
	exit 1
fi
# dotnet pack -c Release --version-suffix rc1
dotnet pack -c Release

# Push packages to nuget.org
find . -wholename '*/Release/*.nupkg' -execdir dotnet nuget push --skip-duplicate {} \;