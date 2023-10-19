import pytest
from src.tiny666 import tiny666

def test_tiny666():
    """Tests the tiny666 function."""
    input = "Hello, world!"
    output = tiny666(input)
    assert len(output) == 666
    for char in output:
        assert char in input
