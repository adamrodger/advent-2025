Advent of Code 2025
===================

Solutions to [Advent Of Code 2025](http://adventofcode.com/2025)

![](https://github.com/adamrodger/advent-2025/workflows/Build%20and%20Test/badge.svg)

Encrypted Inputs
----------------

Note that inputs are included in the repository so that they can be used in CI, but
they are encrypted using `git-crypt`. To use this repository on a new machine, you will
need access to the base64 encoded key, then you can run:

```bash
echo $GIT_CRYPT_KEY | base64 --decode > git-crypt.key
git crypt unlock ./git-crypt.key
rm ./git-crypt.key
```

