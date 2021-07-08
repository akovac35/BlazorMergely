# Authors
#	Aleksander Kovač
#
# This script will add all Release packages to local nuget folder. Projects can be excluded with the following setting:
#  <PropertyGroup>
#    <IsPackable>false</IsPackable>
#  </PropertyGroup>
#
# References
# https://docs.microsoft.com/en-us/nuget/hosting-packages/local-feeds
# https://docs.microsoft.com/en-us/nuget/reference/cli-reference/cli-ref-add

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

# Push packages to local nuget folder
find . -wholename '*/Release/*.nupkg' -execdir nuget add -source /d/tmp/nugets/ {} \;