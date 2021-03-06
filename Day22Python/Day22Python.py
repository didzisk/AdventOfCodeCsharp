import sys; sys.dont_write_bytecode = True; 
from utils import *

def do_case(inp: str, sample=False):
    # READ THE PROBLEM FROM TOP TO BOTTOM OK
    def sprint(*a, **k): sample and print(*a, **k)
    lines = inp.splitlines()
    cards = 119315717514047
    repeats = 101741582076661

    def inv(n):
        # gets the modular inverse of n
        # as cards is prime, use Euler's theorem
        return pow(n, cards-2, cards)
    def get(offset, increment, i):
        # gets the ith number in a given sequence
        return (offset + i * increment) % cards
    
    # increment = 1 = the difference between two adjacent numbers
    # doing the process will multiply increment by increment_mul.
    increment_mul = 1
    # offset = 0 = the first number in the sequence.
    # doing the process will increment this by offset_diff * (the increment before the process started).
    offset_diff = 0
    for line in inp.splitlines():
        if line == "deal into new stack":
            # reverse sequence.
            # instead of going up, go down.
            increment_mul *= -1
            increment_mul %= cards
            # then shift 1 left
            offset_diff += increment_mul
            offset_diff %= cards
        elif line.startswith("cut"):
            q, *_ = ints(line)
            # shift q left
            offset_diff += q * increment_mul
            offset_diff %= cards
        elif line.startswith("deal with increment "):
            q, *_ = ints(line)
            # difference between two adjacent numbers is multiplied by the
            # inverse of the increment.
            increment_mul *= inv(q)
            increment_mul %= cards

    def get_sequence(iterations):
        # calculate (increment, offset) for the number of iterations of the process
        # increment = increment_mul^iterations
        increment = pow(increment_mul, iterations, cards)
        # offset = 0 + offset_diff * (1 + increment_mul + increment_mul^2 + ... + increment_mul^iterations)
        # use geometric series.
        offset = offset_diff * (1 - increment) * inv((1 - increment_mul) % cards)
        offset %= cards
        return increment, offset

    increment, offset = get_sequence(repeats)
    print(get(offset, increment, 2020))
    
    return  # RETURNED VALUE DOESN'T DO ANYTHING, PRINT THINGS INSTEAD

do_case("""deal with increment 65
deal into new stack
deal with increment 25
cut -6735
deal with increment 3
cut 8032
deal with increment 71
cut -4990
deal with increment 66
deal into new stack
cut -8040
deal into new stack
deal with increment 18
cut -8746
deal with increment 42
deal into new stack
deal with increment 17
cut -8840
deal with increment 55
cut -4613
deal with increment 10
cut -5301
deal into new stack
deal with increment 21
cut -5653
deal with increment 2
cut 5364
deal with increment 72
cut -3468
deal into new stack
cut -9661
deal with increment 63
cut 6794
deal with increment 43
cut 2935
deal with increment 66
cut -1700
deal with increment 6
cut 5642
deal with increment 64
deal into new stack
cut -5699
deal with increment 43
cut -9366
deal with increment 42
deal into new stack
cut 2364
deal with increment 13
cut 8080
deal with increment 2
deal into new stack
cut -9602
deal with increment 51
cut 3214
deal into new stack
cut -2995
deal with increment 57
cut -8169
deal into new stack
cut 362
deal with increment 41
cut -4547
deal with increment 56
cut -1815
deal into new stack
cut 1554
deal with increment 71
cut 2884
deal with increment 44
cut -2423
deal with increment 4
deal into new stack
deal with increment 20
cut -2242
deal with increment 48
cut -716
deal with increment 29
cut -6751
deal with increment 45
cut -9511
deal with increment 75
cut -440
deal with increment 35
cut 6861
deal with increment 52
cut -4702
deal into new stack
deal with increment 28
cut 305
deal with increment 16
cut 7094
deal into new stack
cut 5175
deal with increment 30
deal into new stack
deal with increment 61
cut 1831
deal into new stack
deal with increment 25
cut 4043""")