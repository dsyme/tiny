#include <stdio.h>
#include <stdlib.h>
#include <assert.h>
#include "squirrels.h"

// A function that tests the random_color function
void test_random_color() {
  // Call the function 100 times and check that the return value is between 0 and 255
  for (int i = 0; i < 100; i++) {
    int color = random_color();
    assert(color >= 0 && color < 256);
  }
  printf("test_random_color passed\n");
}

// A function that tests the print_squirrel function
void test_print_squirrel() {
  // Call the function and check that the output matches the expected ASCII art
  // Redirect the standard output to a temporary file
  FILE *tmp = freopen("tmp.txt", "w", stdout);
  // Call the function
  print_squirrel();
  // Close the standard output
  fclose(stdout);
  // Reopen the temporary file for reading
  tmp = fopen("tmp.txt", "r");
  // Define the expected output as a string
  char *expected = "\033[38;2;255;192;203m   _   _\n  (q\\_/p)\n   /. .\\    (\n=\\_t_/=   __)\n /   \\   (\n((   ))   )\n=\\_/=   (\n /   \\  (\n((   ))  )\n=\\_/=   (\n /   \\__/\n((___))\n\033[0m";
  // Define a buffer to store the actual output
  char buffer[256];
  // Read the temporary fle into the buffer
  fread(buffer, sizeof(char), 256, tmp);
  // Close and delete the temporary file
  fclose(tmp);
  remove("tmp.txt");
  // Compare the expected and actual output
  assert(strcmp(expected, buffer) == 0);
  printf("test_print_squirrel passed\n");
}

int main() {
  // Run the tests
  test_random_color();
  test_print_squirrel();
  printf("All tests passed\n");
  return 0;
}
