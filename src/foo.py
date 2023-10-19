# A python file that imports and uses the bar module from the inner package

from inner import bar

def foo():
    # A function that does some calculation and prints the result
    x = bar.bar(10)
    y = x * 2
    print(f"The result is {y}")

if __name__ == "__main__":
    # The main entry point of the script
    foo()
