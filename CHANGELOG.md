# Changelog

All notable changes to this project will be documented in this file.

## [1.2.1] - 2026-05-16

### Fixed
- `StatisticEntryType` no longer uses `Unsafe.As` internally, so assemblies that reference this package no longer need `allowUnsafeCode` enabled.
- The runtime assembly is no longer auto-referenced; consuming asmdefs must now reference `Calluna.Statistics` explicitly rather than having it injected automatically.
