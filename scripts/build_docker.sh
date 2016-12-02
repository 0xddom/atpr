#!/usr/bin/env bash

IMAGE=kuroaku/atpr:latest
APP_DIR=`pwd`

[ "$BUILD_DOCKER" == 'yes' ] && docker build -t $IMAGE $APP_DIR
