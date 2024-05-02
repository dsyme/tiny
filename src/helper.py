# This file contains a helper function
import unittest

def greet():
    """Prints a greeting message"""
    print("Hello, world!")

class TestGreet(unittest.TestCase):
    """Tests the greet function"""

    def test_greet(self):
        """Tests that the greet function prints the expected message"""
        # Capture the output of the greet function
        output = []
        def mock_print(*args):
            output.append(args[0])
        helper.print = mock_print
        # Call the greet function
        greet()
        # Check that the output matches the expected message
        self.assertEqual(output, ["Hello, world!"])

if __name__ == "__main__":
    unittest.main()
