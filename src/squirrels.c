#include <stdio.h>
#include <stdlib.h>
#include <time.h>

// A function that generates a random number between 0 and 255
int random_color() {
  return rand() % 256;
}

// A function that prints a pink squirrel using ASCII art and ANSI escape codes
void print_squirrel() {
  // Set the foreground color to pink (255, 192, 203)
  printf("\033[38;2;255;192;203m");
  // Print the squirrel
  printf("   _   _\n");
  printf("  (q\\_/p)\n");
  printf("   /. .\\    (\n");
  printf("=\\_t_/=   __)\n");
  printf(" /   \\   (\n");
  printf("((   ))   )\n");
  printf("=\\_/=   (\n");
  printf(" /   \\  (\n");
  printf("((   ))  )\n");
  printf("=\\_/=   (\n");
  printf(" /   \\__/\n");
  printf("((___))\n");
  // Reset the foreground color
  printf("\033[0m");
}

int main() {
  // Initialize the random seed
  srand(time(NULL));
  // Generate and display 10 pink squirrels
  for (int i = 0; i < 10; i++) {
    print_squirrel();
    printf("\n");
  }
  return 0;
}
