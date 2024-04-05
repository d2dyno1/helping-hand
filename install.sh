#!/bin/sh
set -e

root="src/PomocKolezenska/wwwroot/lib"
rm -f "$root/*"
mkdir "$root/bootstrap"
mkdir "$root/bootstrap-icons"
mkdir "$root/jquery"

npm i

cp node_modules/bootstrap/dist/css/bootstrap.min.css "$root/bootstrap"
cp node_modules/bootstrap/dist/js/bootstrap.bundle.min.js "$root/bootstrap"

cp node_modules/bootstrap-icons/font/bootstrap-icons.min.css "$root/bootstrap-icons"
cp -r node_modules/bootstrap-icons/font/fonts "$root/bootstrap-icons"

cp node_modules/jquery/dist/jquery.min.js "$root/jquery"