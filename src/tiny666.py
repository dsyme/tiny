import random

def tiny666(input):
    """Returns a string of 666 characters, randomly chosen from the input."""
    output = ""
    for _ in range(666):
        output += random.choice(input)
    return output
