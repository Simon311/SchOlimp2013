<?php
$c = trim(fgets(STDIN)); // Текущий этаж
$m = trim(fgets(STDIN)); // На сколько опуститься
$p = trim(fgets(STDIN)); // На сколько подняться
while ($m > 0) // Пока спускаемся
{
	$c--; // Спускаемся на один этаж.
	if ($c == 0) $c--;  // Если этаж 0, то пропускаем.
	$m--; // Убавляем счетчик спусков.
}
while ($p > 0) // Пока поднимаемся
{
	$c++; // Поднимаемся на один этаж.
	if ($c == 0) $c++;  // Если этаж 0, то пропускаем.
	$p--; // Убавляем счетчик подъемов.
}
echo $c; // Выводим.
// Прошла все тесты.
?>