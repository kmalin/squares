# Square API

## Solution

This repository contains solution to a Square detection exercise.

Main algorithm for detecting squares is implemented in a class ***SquareDetector***.

Algorithm runs in a following way:
* Every point list modification (Import, Add, Remove) causes square recalculation.
* When recalculating every pair of points is checked.
* Pair of points is assumed to be a diagonal of a square (points are named A and C).
* For every pair of points (diagonal) other two points are calculated using simple middle point and X and Y difference calculation (points B and D).
* Detected points (B and D) are checked to be valid integers and to exist in a list of points.
* If points B and D exist then square can be formed and it is saved into a list of squares.
* Every square is identified by normalizing (ordering in ascending order by X then Y coordinates) its points.
* Detected squares are saved into cached list for fast retrieval.

### Decisions made and Notes

1. Persistence layer was implemented using both InMemory and Redis repositories. InMemory persistence mode is selected by default in order to provide smoother first run. Redis persistence mode can be enabled by switching to Redis services in Program.cs file.
2. Square detection algorithm is O(n)^2 because all pairs need to be investigated. 
3. Algorithm completes within 5 seconds when processing up to 4000 points. Can be checked in a test SquareDetectorPerformanceTest.
4. Solution contains 24 automated tests for main logic.

## Running of Solution

Solution should run as is from Visual Studio. To enable Redis persistence comment lines 14-15 and uncomment lines 18-20 lines in file Program.cs. When running Redis persistence make sure that Redis is accessible from running application (Docker container).