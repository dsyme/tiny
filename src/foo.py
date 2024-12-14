# A modified python file that imports the git-tesseract class and uses it to create a higher-dimensional codebase object
from inner.baz import git_tesseract

# create a list of files and directories in the codebase
codebase = ["README.md", "src/foo.py", "src/inner/bar.py", "src/inner/baz.py"]

# create a git-tesseract object with the codebase
gt = git_tesseract(codebase)

# print the git-tesseract object
print(gt)

# rotate the git-tesseract object along the (1, 3) axis by pi/2 radians
gt_rotated = gt.rotate((1, 3), 3.14159 / 2)

# print the rotated git-tesseract object
print(gt_rotated)

# project the rotated git-tesseract object onto the 2nd dimension
gt_projected = gt_rotated.project(2)

# print the projected git-tesseract object
print(gt_projected)
