# A new file that defines a git-tesseract class
class git_tesseract:
    def __init__(self, codebase):
        # codebase is a list of files and directories
        self.codebase = codebase
        self.dimension = 4 # the number of dimensions of the git-tesseract

    def rotate(self, axis, angle):
        # rotate the codebase along a given axis by a given angle
        # axis is a tuple of two integers representing the dimensions to rotate
        # angle is a float representing the radians to rotate
        # returns a new git-tesseract object with the rotated codebase
        pass

    def project(self, dimension):
        # project the codebase onto a lower dimension
        # dimension is an integer representing the dimension to project onto
        # returns a new git-tesseract object with the projected codebase
        pass

    def __str__(self):
        # return a string representation of the codebase
        return "\n".join(self.codebase)
