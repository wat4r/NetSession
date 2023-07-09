# NetSession
[![License](https://img.shields.io/badge/License-Apache_2.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
[![Release](https://img.shields.io/github/release/wat4r/NetSession)](https://github.com/wat4r/NetSession/releases)


## Introduction
NetSession is a tool for enumerating remote service connection sessions using the NetSessionEnum function.


## Features
 - Tiny binaries built on C# (≈7.50kb)
 - Simple and readable output format


## Usage
```sh
NetSession [ServerName]
```

## Example
Establish an IPC connection
```bash
net use \\192.168.1.111 /u:administrator password
```

Enumerate client sessions using NetSession
```bash
NetSession.exe 192.168.1.111
```

Output
```console
[+] Enumerating Host: 192.168.1.111
Client              UserName            OpensNum  Time                IdleTime
-------------------------------------------------------------------------------
192.168.1.199       administrator       1         1m:46s              0s
```