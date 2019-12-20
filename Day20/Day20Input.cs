﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Day20
{
    public static class Day20Input
    {
        public static string Ex1 =
@"         A           
         A           
  #######.#########  
  #######.........#  
  #######.#######.#  
  #######.#######.#  
  #######.#######.#  
  #####  B    ###.#  
BC...##  C    ###.#  
  ##.##       ###.#  
  ##...DE  F  ###.#  
  #####    G  ###.#  
  #########.#####.#  
DE..#######...###.#  
  #.#########.###.#  
FG..#########.....#  
  ###########.#####  
             Z       
             Z       ";
        public static string Official =
@"                                 C X   J         X     B         U       N     J                                 
                                 N Z   I         Y     U         D       U     M                                 
  ###############################.#.###.#########.#####.#########.#######.#####.###############################  
  #...#.#.#.......#...#...#...........#...#.#...#...#...#.#...#...#.........#.....#...#.#.......#.......#.#.#.#  
  ###.#.#.#.#.###.###.###.#####.#######.###.#.#####.#.###.#.#.###.#.#.#.#.#######.#.###.#.###.###.#######.#.#.#  
  #.#...#...#.#.#...#...#.#.......#...#.#.#...#.....#.#...#.#...#.#.#.#.#.#.#.....#...#...#.#...#.#...#...#...#  
  #.###.#.#####.#.###.#.#.#.#####.#.#.#.###.#.###.###.###.#.###.#.#.#######.###.###.###.###.#####.#.#####.#.###  
  #.......#...#.#...#.#.....#.#...#.#.#.#...#.#.....#.....#...#...#.#.....#...#...#.....#.#.....#.#...#.#.....#  
  #.###.#.###.#.###.#####.#.#.#.###.#.#.#.###.#.###.#####.#.#####.#.#.###.#.#.###.#.###.#.#.###.#.#.###.###.###  
  #.#...#.#.....#.#...#.#.#.#.......#.#...#...#.#.#.#.#...#...#...#.....#.#.#.......#.....#...#.....#.#.......#  
  #.###########.#.###.#.#############.#####.#.#.#.###.#.###.#####.#.#.###.#.###.###.###.#.#.#########.###.#####  
  #.#...........#...#.............#...#.#...#.#.....#...#.#.....#.#.#.#.#.#.#...#.#.#.#.#.....#.#...#.........#  
  #########.###.###.#######.###.###.###.#.#####.#######.#.###.#########.#.#.###.#.###.#.###.###.###.###.###.#.#  
  #...#.#.#.#...#.#.#.#.....#.#.......#.......#...#.#.....#.....#.....#...#...#.....#.....#.#...#.#.......#.#.#  
  #.###.#.#####.#.#.#.###.###.###.#.#.###.#####.###.#.#####.###.#####.#.#.#.#################.###.###.#.#.#####  
  #.............#.....#...#.#.....#.#.#.#.....#...#.....#...#.....#...#.#.#.....#.#...#.....#.#...#...#.#...#.#  
  #############.###.#.#.###.###.#.#####.#.#####.#.###.###.#.###.#####.###.#.###.#.#.#####.#.#.###.###.#######.#  
  #.#.....#.....#.#.#.#.#.......#.#...#.......#.#.#...#.#.#...#.#...#.#...#...#.#.#...#.#.#.....#.#.#.#.#.#...#  
  #.#####.#####.#.###.#######.#.###.#.#.#######.#####.#.###.#.#.###.#.#.###.#####.###.#.###.#.#.#.#.#.#.#.#.#.#  
  #.....#.#.............#.#...#...#.#.......#.#...#.....#...#.#.#.#.......#.#.......#.......#.#...........#.#.#  
  #####.#.#.###.###.#.###.#####.#########.###.###.###.###.#.#####.#######.#.###.#####.###.#.#########.#######.#  
  #.#.#.......#.#...#.................#.....#.......#...#.#.......#.#.....#...#...#.....#.#.....#.......#...#.#  
  #.#.###.#.#####.###.#.#.###.#.###.###.###.#.#.#####.#####.###.###.#.###.#.###.###.###############.#.#.###.#.#  
  #.#...#.#.#.....#...#.#...#.#.#.#.#...#.#.#.#.....#.....#.#...#.#.....#.#.....................#...#.#.#.#...#  
  #.###.#############.###########.#.###.#.#######.#####.#######.#.###.#.#########.#######.###.#.#.#####.#.#.###  
  #.#...#...#.#.#...#.....#...........#...#.#.........#.....#.#.....#.#.#.#.........#.#.#.#...#.#.#.#.#.#.....#  
  #.###.#.###.#.#.#####.###.#.#######.#.#.#.###.###.###.#.###.###.#.###.#.###.#.###.#.#.###########.#.#######.#  
  #...#.#.......#.........#.#.#.......#.#.....#...#...#.#.#.......#.#.....#...#...#.#...#.....#...#...........#  
  #.###.#.#########.#.###.#.#######.#.#######.###.#######.#.#############.#.###########.###.###.#############.#  
  #.#.........#.....#.#.#.#.#      M E       U   H       U D             V X        #.#.#.....#.....#.#.#.#...#  
  #.###.###############.#####      F Y       D   L       I R             E Z        #.#.#.#######.###.#.#.###.#  
  #.#.....#.#...#.....#.#.#.#                                                       #.......#.#.#.#...#.#...#.#  
  #.#####.#.###.#.#####.#.#.#                                                       ###.###.#.#.#.#.###.###.#.#  
  #.#.#...#...#...#...#.#...#                                                       #...#.#.....#...#.#.....#.#  
  #.#.###.#.#####.###.#.#.###                                                       ###.#.#.#######.#.#####.#.#  
  #.............#.......#...#                                                       #.#...#...#.#.#...#.#......VE
  #.#######.#####.###.###.###                                                       #.###.#.###.#.###.#.#.#.###  
  #...#.........#...#...#.#.#                                                     TJ......#...............#...#  
  ###.#.#########.#.#####.#.#                                                       #.###.#########.###########  
DR..#.#...#.....#.#.#...#.#..HP                                                     #.#...#.#.#.#.#.#.........#  
  #.#.###.#.###.#.#.###.#.#.#                                                       #.###.#.#.#.#.###.#####.###  
  #.....#...#.#...#.........#                                                       #...#.#...#.#.......#...#.#  
  #######.###.###############                                                       #######.###.#####.###.###.#  
  #.....#.#.......#.#.....#.#                                                       #.#...#.#.#.#...#...#.#....TJ
  #.###.###.###.#.#.#.###.#.#                                                       #.###.#.#.#.#.###.###.#.###  
HP..#.....#...#.#...#...#...#                                                     BU..................#.....#.#  
  #####.###.#####.#.###.#.#.#                                                       #########.#########.#####.#  
  #.........#.....#...#.#.#.#                                                     TC........#.#.#...#.#.#.....#  
  ###########.###.#.#.#.###.#                                                       ###.###.###.#.#.#.#####.#.#  
  #.#.#.....#.#.#.#.#...#.#..FN                                                     #...#.........#...#.....#.#  
  #.#.###.#####.#########.###                                                       #########.#####.#.#.#####.#  
MF..#.#...#.....#...#...#....XY                                                     #...#.#...#.#.#.#.....#.#..FN
  #.#.#.#.#####.#.###.#.#.###                                                       ###.#.#####.#.#####.###.###  
  #.#...#.#.#.........#.#.#.#                                                       #.................#.#.#...#  
  #.#.#.###.###.#.###.#.#.#.#                                                       #.###.###.#.#.###.###.#.#.#  
  #...#.........#...#.#.....#                                                       #.#.....#.#.#.#.#.....#.#..WI
  ###############.#########.#                                                       #.#####.#.#.#.#.#####.#.###  
  #...........#...#...#.#...#                                                     EH..#.#...#.#.#.#...#...#...#  
  #.#####.###.#####.###.#####                                                       ###.#############.#.#####.#  
  #.#...#.#.....#.#.......#.#                                                       #.....#.#...#...#.........#  
  #.#######.#.###.#.###.###.#                                                       ###.###.###.###.#########.#  
  #.......#.#.#.....#...#....YW                                                     #.......#...#...........#.#  
  #####.###.#####.#.###.###.#                                                       #.###.###.#.#.###.#.#######  
TA........#.......#.#.......#                                                     MR..#.#...#.#.....#.#...#....EY
  ###.#.#####.#####.###.#####                                                       #.#.#.###.###########.###.#  
  #...#.#...#.#.....#.......#                                                       #.#.#...#.....#.#.#.......#  
  #######.#.#############.#.#                                                       ###.#.#.###.###.#.#####.###  
  #...#...#.#.#.....#...#.#.#                                                       #...#.#.....#...#.#.#...#.#  
  #.###.###.#.#.#######.#####                                                       ###.###########.#.#.#####.#  
MR....#...#.......#.....#.#.#                                                       #.......#...............#..FM
  #.#####.#####.###.#####.#.#                                                       #.#.#.###.###.#.#####.###.#  
  #.........#................NU                                                   JI..#.#...#.#.#.#...#...#...#  
  ###########################                                                       #.#.#.###.#.#.#######.#.###  
  #...#.#...........#.#.....#                                                       #.#.#...#...#.....#...#...#  
  #.#.#.###.#####.#.#.###.#.#                                                       #######.#.###########.###.#  
ZZ..#.....#.....#.#.#.....#.#                                                       #.#.#.......#...#...#.....#  
  #.#####.#.#######.###.###.#                                                       #.#.#########.#####.#######  
  #...#.....#.#.#.#.....#.#..TA                                                     #.#.#.............#.....#..BW
  #.###.###.#.#.#.#.#.#.#.#.#                                                       #.#.#.#.#.#.#.###.#.###.#.#  
UI....#.#.......#...#.#.#...#                                                       #.....#.#.#.#.#.......#...#  
  #.###.#.#####.#####.#.#####                                                       #.#.###.#####.#####.###.#.#  
  #.#...#.#.#.....#...#.....#                                                     HM..#.#.....#.....#...#...#.#  
  #.#######.#.#######.#######                                                       #.#####.#####.#####.###.#.#  
  #.....#.......#.....#...#.#                                                       #...#...#...#...#.....#.#.#  
  #.#######.#.###.#.#.#.#.#.#      C       F         W         J   B     F          #.#########.#.###.#####.###  
  #.#.#.#.#.#.#...#.#.#.#.#.#      N       M         I         M   W     A          #...#.........#...#.......#  
  #.#.#.#.#####.#####.###.#.#######.#######.#########.#########.###.#####.###########.#######.###.###.#####.###  
  #.........#.....#...#.#.....#.#.#.....#...........#...#.....#.#...#...............#.......#...#.#.....#.....#  
  ###.#.#.#.#.#.###.###.#.#.#.#.#.#.###.#.#.###.#######.#.###.#.#.#######.###.#.#.#.###.#####.#.###.#####.#.#.#  
  #...#.#.#.#.#.#.....#...#.#.........#.#.#...#.#.#.#...#...#.#.#.#...#.#.#.#.#.#.#...#...#...#.#...#.....#.#.#  
  ###.#.#.#.#.#.#######.#.#######.#####.#######.#.#.###.###.#.#.#.#.#.#.###.###.#####.#.###.#######.#.###.#.#.#  
  #...#.#.#.#.#.....#...#.#.#.....#.#.....#.#...#.......#...#.#.#...#.......#.....#.#.#.#.......#...#...#.#.#.#  
  #######.#####.###########.#####.#.###.###.#.#.#.#######.###.#.###.###.#######.###.###########.#####.###.###.#  
  #...........#.....#.........#.#.#.......#...#.#.....#...#...#.#.#.#.#.#.#.#...#.#.......#.#.#.#.#...#.....#.#  
  #####.#.###.#.#.###########.#.#######.###.#######.#.###.#.###.#.###.#.#.#.###.#.#.#.#.###.#.###.#####.#.#.#.#  
  #.....#...#.#.#.#...#.#.#.......#.......#.....#.#.#...#.#.......#.......#...#.....#.#.............#.#.#.#.#.#  
  #.#.#.###.###.#####.#.#.#######.###.#####.#.#.#.#####.#.#.###.#####.#######.#.###########.#.#####.#.###.#.#.#  
  #.#.#...#.#.#.#.....#...............#.#.#.#.#...#.#.#.#.#.#...#...........#.....#.#.#...#.#.....#.....#.#.#.#  
  #.###.###.#.#######.#.#.#####.#.###.#.#.#####.#.#.#.#.#.#####.#######.#####.#####.#.#.#.###.#.#########.#.#.#  
  #...#.#.........#.....#...#.#.#...#.......#.#.#...#...#.....#.#...#...#.#.#.#.#...#.#.#...#.#.......#...#.#.#  
  #.###.###.#.###.#.###.#####.#########.#####.#.#######.#.#######.###.#.#.#.#.#.###.#.#.#######.###.#.###.#.#.#  
  #.#.....#.#.#...#.#...#.#.#.#.#.#...#.....#.....#...#.#.......#...#.#.#...............#.#...#.#.#.#.#...#.#.#  
  #.#.#####.#.#####.#####.#.#.#.#.###.#.#######.###.###.#.#########.###.#.#.#.#.###.#.###.#.#####.###.###.#.###  
  #.#.....#.#...#...#.#.#.#.....#.#.#.#...#.....#.......#.....#.#.#.#.#.#.#.#.#...#.#.#.............#...#.#...#  
  ###.#####.#.#####.#.#.#.#.###.#.#.#.#.#######.#.###.###.#####.#.#.#.#.#.#########.###.###.###.#########.###.#  
  #...#.....#.....#.#.......#...........#.#.#...#...#.#.....#...........#.#.#.#.........#...#.#.#...#.#.#.#...#  
  #.#####.###.#####.#############.#.#.###.#.#.#.#.#.#.###.#######.#####.#.#.#.#####.#########.###.###.#.#.#####  
  #...#.....#...#...#.............#.#...#.....#.#.#.#.#.......#.#.....#.#.#.....#...#.....#.#...#.....#.#.....#  
  #.#####.#.###########.#.#.###.#######.#.#######.###.###.#####.###.#####.#.#####.#.#.#.#.#.#.###.#.###.#######  
  #...#...#...#.........#.#.#...#.......#.....#...#...#.#...#...#.......#.#.#...#.#...#.#.........#...........#  
  #.#######.#####.#.#####.#.#####.#####.#####.###.#####.#.###.#.#.###.###.#.###.###.#.#####.###.#.###.#.###.#.#  
  #.#.........#...#.....#.#.#.....#.......#.....#.....#.......#.#...#.#...........#.#.....#...#.#.#...#...#.#.#  
  #####################################.###.#######.#########.###.#.#######.###################################  
                                       H   Y       H         F   A E       T                                     
                                       M   W       L         A   A H       C                                     ";
    }
}
