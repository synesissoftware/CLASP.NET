#! /usr/bin/env bash
set -euo pipefail

RootDir="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ProjectNameFile="${RootDir}/.sis/project_name.txt"
ProjectName=$(tr -d '[:space:]' < "${ProjectNameFile}")
Configuration="${1:-Release}"
Artifacts="${RootDir}/artifacts"

cd "${RootDir}"

echo "${ProjectName}: restore / build / test / pack (${Configuration})"

dotnet restore CLASP.NET.sln
dotnet build CLASP.NET.sln --configuration "${Configuration}" --no-restore
dotnet test CLASP.NET.sln --configuration "${Configuration}" --no-build --verbosity normal

mkdir -p "${Artifacts}/packages"
dotnet pack src/CLASP.NET/CLASP.NET.csproj \
  --configuration "${Configuration}" \
  --no-build \
  --output "${Artifacts}/packages"

echo "${ProjectName}: packages written to ${Artifacts}/packages"
