import unittest
from io import StringIO
import sys
from src.main import print_hello_world

class TestMain(unittest.TestCase):
    def test_print_hello_world(self):
        captured_output = StringIO()
        sys.stdout = captured_output
        print_hello_world()
        sys.stdout = sys.__stdout__
        self.assertEqual(captured_output.getvalue().strip(), "Hello, World!")

if __name__ == '__main__':
    unittest.main()
