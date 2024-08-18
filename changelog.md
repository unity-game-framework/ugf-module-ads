# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0-preview.2](https://github.com/unity-game-framework/ugf-module-ads/releases/tag/1.0.0-preview.2) - 2024-08-18  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-ads/milestone/3?closed=1)  
    

### Changed

- Update package ([#3](https://github.com/unity-game-framework/ugf-module-ads/issues/3))  
    - Update dependencies: `com.ugf.application` to `9.0.0-preview`, remove `com.unity.modules.androidjni` package.
    - Update package _Unity_ version to `2023.2`.
    - Update package registry to _UPM Hub_.
    - Change `AdsModule` and related classes to support update _Application_ package.
    - Change `AdsUnityModule` and related classes to only work when _LevelPlay_ package installed.
    - Remove _LevelPlay_ plugin direct installation, use package instead.

## [1.0.0-preview.1](https://github.com/unity-game-framework/ugf-module-ads/releases/tag/1.0.0-preview.1) - 2023-05-08  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-ads/milestone/2?closed=1)  
    

### Removed

- Remove dependency resolver ([#4](https://github.com/unity-game-framework/ugf-module-ads/issues/4))  
    - Remove _MobileDependencyResolver_ plugin.

## [1.0.0-preview](https://github.com/unity-game-framework/ugf-module-ads/releases/tag/1.0.0-preview) - 2023-04-05  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-ads/milestone/1?closed=1)  
    

### Added

- Add implementation ([#1](https://github.com/unity-game-framework/ugf-module-ads/issues/1))  
    - Add `IAdsModule` interface as abstract access to the ads module.
    - Add `AdsUnityModule` class as default implementation of _Unity IronSource_ ads.


