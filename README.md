# ambidextrous

[![GitHub release](https://img.shields.io/github/release/Nanofus/ambidextrous.svg)]() [![npm version](https://badge.fury.io/js/ambidextrous.svg)](https://badge.fury.io/js/ambidextrous)

A mouse button swapping utility for Windows. Dedicated to all of you lefties and ambidextrous people out there, who like to change your mouse hand once in a while. Ambidextrous natively changes the Windows setting for primary mouse button.

## Installation

Download the [latest](https://github.com/Nanofus/ambidextrous/releases/latest) release `.zip` or use `npm`:
```
npm install ambidextrous
```
Save Ambidextrous in your environment variables to call it from anywhere:
```
ambi path
```
Set Ambidextrous to run at Windows startup:
```
ambi startup
```

## Usage
To swap the mouse buttons:
```
ambi
```
To start a background listener that swaps the buttons on Alt+K (configurable in the future):
```
ambi listen
```
To stop Ambidextrous from running at Windows startup:
```
ambi startup remove
```
To see information about the application:
```
ambi help
```

## License
Ambidextrous is licensed under the MIT License. Copyright © Ville Talonpoika 2016
