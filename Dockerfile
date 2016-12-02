FROM ubuntu:16.04
MAINTAINER Daniel Domínguez <danieldominguez05@gmail.com>

# Setup base system
RUN apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF && \
echo "deb http://download.mono-project.com/repo/debian wheezy main" > /etc/apt/sources.list.d/mono-xamarin.list && \
apt-get update && apt-get install -y apt-utils

RUN apt-get install -y mono-devel nuget

# Install Stanford libraries
RUN apt-get install -y wget

RUN wget 'https://www.dropbox.com/s/103t3z1tiiquizq/Standford.tar.gz?dl=0' -o stanford.tar.gz

RUN tar -xvvf stanford.tar.gz

# Add and compile app
ADD . /app

WORKDIR /app

RUN nuget restore && xbuild /p:Configuration=Release

RUN ln -s /app/ATPR/bin/Release/ATPR.exe /bin/atpr
