#!/usr/bin/env bash

IMAGE=kuroaku/atpr:latest
APP_DIR=`pwd`

[ "$BUILD_DOCKER" == 'no' ] && exit 0 # Exit cleanly in case this is not needed

[ "$BUILD_DOCKER" == 'yes' ] && docker build -t $IMAGE $APP_DIR
