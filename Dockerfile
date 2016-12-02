FROM ubuntu:16.04
MAINTAINER Daniel Domínguez <danieldominguez05@gmail.com>

# Setup base system
RUN apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF && \
echo "deb http://download.mono-project.com/repo/debian wheezy main" > /etc/apt/sources.list.d/mono-xamarin.list && \
apt-get update && apt-get install -y apt-utils

RUN apt-get install -y mono-devel nuget

# Install Stanford libraries
RUN apt-get install -y wget

RUN wget 'https://www.dropbox.com/s/103t3z1tiiquizq/Standford.tar.gz?dl=0' -O stanford.tar.gz
RUN wget 'https://www.dropbox.com/s/42iympdp1ei8ow4/checksum.sha1.txt?dl=0' -O checksum.sha1

RUN cat checksum.sha1 && shasum -c checksum.sha1 && tar -xvvf stanford.tar.gz && mv Standford stanford

# Add and compile app
ADD . /app

WORKDIR /app

RUN nuget restore && xbuild /p:Configuration=Release

RUN chmod +x atpr.sh && ln -s /app/atpr.sh /bin/atpr

