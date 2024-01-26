#!/bin/sh
set -e

wwwroot="src/dotnetBB/wwwroot"

rm -f "$wwwroot/bootstrap/*"
rm -f "$wwwroot/bootstrap-icons/*"

npm i
cp node_modules/bootstrap/dist/css/bootstrap.min.css "$wwwroot/bootstrap"
cp node_modules/bootstrap/dist/js/bootstrap.bundle.min.js "$wwwroot/bootstrap"
cp node_modules/bootstrap-icons/font/bootstrap-icons.min.css "$wwwroot/bootstrap-icons"
cp -r cp node_modules/bootstrap-icons/font/fonts "$wwwroot/bootstrap-icons"