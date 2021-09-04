// Test_Mpir.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include "mpir.h"
#include "conio.h"
#include < stdio.h>
#include < stdlib.h>
#include "mpir.h"
#include < iostream>
#include <ctime>
#include <chrono>
#include <cmath>
#include <intrin.h>
// решает некоторые ошибки несовместимости версий
extern "C" FILE * __cdecl __iob_func()
{
	struct _iobuf_VS2012 { // ...\Microsoft Visual Studio 11.0\VC\include\stdio.h #56
		char* _ptr;
		int   _cnt;
		char* _base;
		int   _flag;
		int   _file;
		int   _charbuf;
		int   _bufsiz;
		char* _tmpfname;
	};
	// VS2015 has only FILE = struct {void*}

	int const count = sizeof(_iobuf_VS2012) / sizeof(FILE);

	//// stdout
	return (FILE*)(&(__acrt_iob_func(1)->_Placeholder) - count);

	// stderr
	//return (FILE*)(&(__acrt_iob_func(2)->_Placeholder) - 2 * count);
}

using namespace std;

unsigned long int log2(const mpz_t number)
{
	unsigned long int log2 = 0;
	mpz_t val_in_2_deg;
	mpz_init_set_ui(val_in_2_deg, 2); // показатель должен на 1 отсавать

	while (mpz_cmp(number, val_in_2_deg) != -1)
	{
		mpz_mul_2exp(val_in_2_deg, val_in_2_deg, 1); // умножить на 2^1
		log2++;
	}

	mpz_clear(val_in_2_deg);

	return log2;
}
void log2(mpz_t result, const mpz_t number)
{
	mpz_init(result); // = 0
	mpz_t val_in_2_deg;
	mpz_init_set_ui(val_in_2_deg, 2); // показатель должен на 1 отсавать

	while (mpz_cmp(number, val_in_2_deg) != -1)
	{
		mpz_mul_2exp(val_in_2_deg, val_in_2_deg, 1); // умножить на 2^1
		mpz_add_ui(result, result, 1);
	}
	mpz_clear(val_in_2_deg);
}


unsigned long int gcd_evklid(mpz_t res, const mpz_t A, const mpz_t B)
{
	unsigned long int steps = 0;
	mpz_t a, b;
	mpz_init_set(a, A);
	mpz_init_set(b, B);
	while (mpz_sgn(a) == mpz_sgn(b))
	{
		steps++;
		if (mpz_cmp(a, b) == 1)
			mpz_tdiv_r(a, a, b);
		else
			mpz_tdiv_r(b, b, a);
	}
	mpz_add(res, a, b);
	mpz_clear(a);
	mpz_clear(b);

	return steps;
}

void Lfind_ab(mpz_t a, mpz_t b, const mpz_t u, const mpz_t v, const mpz_t k)
{
	mpz_t x1; mpz_init_set(x1, u);
	mpz_t x2; mpz_init_set_ui(x2, 1);
	mpz_t x3; mpz_init_set_ui(x3, 0);

	mpz_t y1; mpz_init_set(y1, v);
	mpz_t y2; mpz_init_set_ui(y2, 0);
	mpz_t y3; mpz_init_set_ui(y3, 1);

	mpz_t z1; mpz_init(z1);
	mpz_t z2; mpz_init(z2);
	mpz_t z3; mpz_init(z3);

	mpz_t u_on_k1; mpz_init(u_on_k1);
	mpz_add_ui(u_on_k1, k, 1);
	mpz_tdiv_q(u_on_k1, u, u_on_k1);

	mpz_t q; mpz_init(q);

	while (mpz_cmp(y1, u_on_k1) == 1)
	{
		mpz_tdiv_q(q, x1, y1);
		mpz_mul(z1, q, y1); mpz_sub(z1, x1, z1);
		mpz_mul(z2, q, y2); mpz_sub(z2, x2, z2);
		mpz_mul(z3, q, y3); mpz_sub(z3, x3, z3);

		mpz_set(x1, y1);
		mpz_set(x2, y2);
		mpz_set(x3, y3);

		mpz_set(y1, z1);
		mpz_set(y2, z2);
		mpz_set(y3, z3);
	}

	mpz_set(a, y2);
	mpz_set(b, y3);

	mpz_clear(x1);
	mpz_clear(x2);
	mpz_clear(x3);
	mpz_clear(y1);
	mpz_clear(y2);
	mpz_clear(y3);
	mpz_clear(z1);
	mpz_clear(z2);
	mpz_clear(z3);
	mpz_clear(u_on_k1);
	mpz_clear(q);
}
unsigned long int gcd2(mpz_t result, const mpz_t u_orig, const mpz_t v_orig)
{
	unsigned long int steps = 0;

	mpz_t u, v;
	mpz_init_set(u, u_orig);
	mpz_init_set(v, v_orig);

	// Параметры
	long int d = 32; // Натуральное 32 или 64
	mpz_t k;
	mpz_init(k);
	mpz_ui_pow_ui(k, 2, d); // Натуральное k = 2^d

	// Чтобы не инициализировать в цикле каждый раз
	mpz_t sub_logs, t, pow_k_in_e, e_mpz_t;
	unsigned long int e;
	mpz_init(sub_logs);
	mpz_init(t);
	mpz_init(pow_k_in_e);
	mpz_init(e_mpz_t);

	mpz_t log2_u; mpz_init(log2_u);
	mpz_t log2_v; mpz_init(log2_v);

	mpz_t a; mpz_init(a);
	mpz_t b; mpz_init(b);
	
	mpz_t b_on_t; mpz_init(b_on_t);

	while (mpz_sgn(u) != 0 && mpz_sgn(v) != 0)
	{
		steps++;

		if (mpz_cmp(v, u) == 1)
			mpz_swap(v, u);

		// Поиск log2(u) и log2(v) и их разности
		log2(log2_u, u);
		log2(log2_v, v);
		//gmp_printf("log2(u)=%Zd log2(v)=%Zd\n", log2_u, log2_v);
		mpz_sub(sub_logs, log2_u, log2_v);
		
		// e = sub_logs / d
		e = mpz_tdiv_q_ui(e_mpz_t, sub_logs, d);

		// k^e
		mpz_pow_ui(pow_k_in_e, k, e);

		// t = v*k^e
		mpz_mul(t, v, pow_k_in_e); // a left shift

		// (a,b) = Lfind_ab(u, t, k)
		Lfind_ab(a, b, u, t, k);
		//gmp_printf("a=%Zd b=%Zd\n", a, b);

		// u = |a*u + b*t|
		mpz_mul(b_on_t, b, t);
		mpz_mul(u, a, u);
		mpz_add(u, u, b_on_t);
		mpz_abs(u, u);

		//gmp_printf("u=%Zd v=%Zd\n", u, v);
	}

	mpz_init(result);
	mpz_add(result, u, v);
	gcd_evklid(result, result, v_orig);
	gcd_evklid(result, result, u_orig);

	mpz_clear(k);
	mpz_clear(e_mpz_t);
	mpz_clear(sub_logs);
	mpz_clear(u);
	mpz_clear(v);
	mpz_clear(log2_u);
	mpz_clear(log2_v);
	mpz_clear(a);
	mpz_clear(b);
	mpz_clear(b_on_t);

	return steps;
}

void find_abcd(mpz_t a, mpz_t b, mpz_t c, mpz_t d, const mpz_t u_extra, const mpz_t v_extra, const mpz_t k)
{
	mpz_t z; mpz_init(z);
	mpz_tdiv_q(z, u_extra, v_extra);

	mpf_t uf; mpf_init(uf); mpf_set_z(uf, u_extra);
	mpf_t vf; mpf_init(vf); mpf_set_z(vf, v_extra);
	mpf_t vf_on_uf; mpf_init(vf_on_uf);

	// z >= 1, т к всегда v' > u'
	mpz_init_set_ui(a, 1);
	mpz_init(b);
	mpz_add_ui(b, z, 1);
	mpz_init_set_ui(c, 1);
	mpz_init_set(d, z);

	mpf_t af; mpf_init(af);
	mpf_t bf; mpf_init(bf);
	mpf_t cf; mpf_init(cf);
	mpf_t df; mpf_init(df);
	mpf_t af_on_bf; mpf_init(af_on_bf);
	mpf_t cf_on_df; mpf_init(cf_on_df);

	mpf_set_z(af, a);
	mpf_set_z(bf, b);
	mpf_set_z(cf, c);
	mpf_set_z(df, d);

	mpf_t left_val; mpf_init(left_val);
	mpz_t k_plus_1; mpz_init(k_plus_1);
	mpf_t k1f; mpf_init(k1f);
	mpf_t right_val; mpf_init(right_val);

	mpf_div(vf_on_uf, vf, uf); // больше не меняется
	mpz_add_ui(k_plus_1, k, 1);// больше не меняется
	mpf_set_z(k1f, k_plus_1); // больше не меняется


	// В цикле: поиск левой границы
	while (true)
	{
		mpf_div(af_on_bf, af, bf);

		mpf_sub(left_val, vf_on_uf, af_on_bf);
		mpf_abs(left_val, left_val); // |v'/u' - p/q|

		mpf_mul(right_val, k1f, bf);
		mpf_ui_div(right_val, 1, right_val); // 1/q*(k+1)
		
		gmp_printf("1cicle a=%.0Ff b=%.0Ff ___ %.7Ff > %.7Ff\n", af, bf, left_val, right_val);
		if (mpf_cmp(left_val, right_val) != 1)
			break;

		mpf_add(af, af, cf); // числитель медианты
		mpf_add(bf, bf, df); // знаменатель медианты
	}

	// В цикле: поиск правой границы
	// В цикле: поиск левой границы
	while (true)
	{
		mpf_div(cf_on_df, cf, df);

		mpf_sub(left_val, vf_on_uf, cf_on_df);
		mpf_abs(left_val, left_val); // |v'/u' - p/q|

		mpf_mul(right_val, k1f, df);
		mpf_ui_div(right_val, 1, right_val); // 1/q*(k+1)

		gmp_printf("2cicle c=%.0Ff d=%.0Ff ___ %.7Ff > %.7Ff\n", cf, df, left_val, right_val);
		if (mpf_cmp(left_val, right_val) != 1)
			break;

		mpf_add(cf, af, cf); // числитель медианты
		mpf_add(df, bf, df); // знаменатель медианты
	}

	mpf_clear(uf);
	mpf_clear(vf);
	mpf_clear(vf_on_uf);

	mpz_set_f(a, af); mpf_clear(af);
	mpz_set_f(b, bf); mpf_clear(bf);
	mpz_set_f(c, cf); mpf_clear(cf);
	mpz_set_f(d, df); mpf_clear(df);
	mpf_clear(af_on_bf);
	mpf_clear(cf_on_df);

	mpf_clear(left_val);

	mpz_clear(k_plus_1);
	mpf_clear(k1f);

	mpf_clear(right_val);

	mpz_clear(z);
}
void gcd(mpz_t result, const mpz_t u_orig, const mpz_t v_orig)
{
	unsigned long int s = 4;
	unsigned long int m = 1;
	mpz_t k;
	mpz_init_set_ui(k, 1);
	mpz_mul_2exp(k, k, m); // Натуральное k = 2^m

	mpz_t u, v;
	mpz_init_set(u, u_orig);
	mpz_init_set(v, v_orig);

	//mpz_t a, b, c, d;
	//find_abcd(a, b, c, d, u_orig, v_orig, k);


	mpz_t v_extra, u_extra;
	mpz_init(v_extra);
	mpz_init(u_extra);

	mpz_t div_ex_u_on_v;
	mpz_init(div_ex_u_on_v);
	mpz_t div_ex_u_on_v_and_mi_1;
	mpz_init(div_ex_u_on_v_and_mi_1);

	mpz_t div_u_ex_on_k_v_ex;
	mpz_init(div_u_ex_on_k_v_ex);

	mpz_t pow_k_in_e;
	mpz_init(pow_k_in_e);

	mpz_t bvk_e, au, dvk_e, cu;
	mpz_init(bvk_e);
	mpz_init(au);
	mpz_init(dvk_e);
	mpz_init(cu);

	// Внутри цикла
	if (mpz_cmp(v, u) == 1)
		mpz_swap(v, u);
	gmp_printf("s=%d\n", s);
	gmp_printf("v=%Zd log2(v)=%d\n",v, log2(v));
	gmp_printf("k=%Zd log2(k)=%d\n",k, log2(k));
	long unsigned int t = log2(v) - s*log2(k); // >= 1
	gmp_printf("t=%d\n", t);

	mpz_div_2exp(v_extra, v, t); // разделить v на 2^t
	mpz_div_2exp(u_extra, u, t); // разделить u на 2^t

	mpz_tdiv_q(div_ex_u_on_v, u_extra, v_extra); // u'/v'
	mpz_sub_ui(div_ex_u_on_v_and_mi_1, div_ex_u_on_v, 1); // u'/v' - 1

	if (mpz_cmp(div_ex_u_on_v_and_mi_1, k) != -1) // u'/v' - 1 >= k
	{
		// Шаг алгоритма Евклида
		mpz_submul(u, div_ex_u_on_v, v); // u = u - (u'/v')*v . 
	}
	else
	{
		// Шаг К-рного алгоритма
		mpz_set_ui(pow_k_in_e, 1);
		mpz_tdiv_q(div_u_ex_on_k_v_ex, u_extra, k);
		mpz_tdiv_q(div_u_ex_on_k_v_ex, div_u_ex_on_k_v_ex, v_extra);
		while (mpz_cmp(div_u_ex_on_k_v_ex, pow_k_in_e) == 1) // v'k^e <= u' <= v'k^(e+1)
			mpz_mul(pow_k_in_e, pow_k_in_e, k); // k^e
		mpz_mul(v_extra, v_extra, pow_k_in_e); // v' = v'*k^e
	
		// find_abcd(u_extra, v_extra, k))
		mpz_t a, b, c, d;
		gmp_printf("u'=%Zd v'=%Zd\n", u_extra, v_extra);
		find_abcd(a, b, c, d, u_extra, v_extra, k);
		gmp_printf("%Zd %Zd %Zd %Zd\n", a, b, c, d);
		
		// чтобы два раза не считать vk^e
		mpz_mul(bvk_e, v, pow_k_in_e);
		// dvk^e
		mpz_mul(dvk_e, bvk_e, d);
		// bvk^e
		mpz_mul(bvk_e, bvk_e, b);

		mpz_mul(au, a, u);
		mpz_mul(cu, c, u);

		// |bvk^e - au|
		mpz_sub(u, bvk_e, au);
		mpz_abs(u, u);
		// |dvk^e - cu|
		mpz_sub(v, dvk_e, cu);
		mpz_abs(v, v);

		mpz_clear(a);
		mpz_clear(b);
		mpz_clear(c);
		mpz_clear(d);

		// u = makeodd(bvk^e-au)
		while (mpz_even_p(u) && mpz_cmp_ui(u, 0) != 0)
			mpz_div_2exp(u, u, 1);
		// v = makeodd(dvk^e-cu)
		while (mpz_even_p(v) && mpz_cmp_ui(v, 0) != 0)
			mpz_div_2exp(v, v, 1);
	}


	// После цикла
	mpz_init(result);
	mpz_add(result, u, v);

	
	mpz_clear(k);
	mpz_clear(u);
	mpz_clear(v);
	mpz_clear(v_extra);
	mpz_clear(u_extra);
	mpz_clear(div_ex_u_on_v);
	mpz_clear(div_ex_u_on_v_and_mi_1);
	mpz_clear(div_u_ex_on_k_v_ex);
	mpz_clear(pow_k_in_e);
	mpz_clear(bvk_e);
	mpz_clear(au);
	mpz_clear(dvk_e);
	mpz_clear(cu);
#pragma endregion
}

int main(int argc, _TCHAR* argv[])
{
	// Состояние для рандома
	gmp_randstate_t state;
	gmp_randinit_mt(state);

	// Числа, у которых будет искаться НОД
	mpz_t A; mpz_init(A);
	mpz_t B; mpz_init(B);
	mpz_t GCD_A_B;

	// Подсчет тактов процесора
	unsigned __int64 start_tick;

	unsigned long int n_array[7] = { 20, 50, 100, 200, 300, 800, 1024};
	
	for(auto n : n_array)
	{
		// 3 пары для заданной длины
		for (int i = 0; i < 3; i++)
		{
			mpz_urandomb(A, state, n);
			mpz_urandomb(B, state, n);

			
			start_tick = __rdtsc();
			// Сдвиг
			auto beg_sh = chrono::steady_clock::now(); // Время перед поискои НОД
			unsigned long int steps_sh = gcd2(GCD_A_B, A, B); // НОД
			auto end_sh = chrono::steady_clock::now(); // Время после поиска НОД
			// Длительность работы в миллисекундах
			auto ms = (unsigned long int) (chrono::duration_cast<chrono::milliseconds>(end_sh - beg_sh)).count();
			gmp_printf("n=%d\nShiftL: gcd(%Zd,%Zd)=%Zd steps=%d %d ms ", n, A, B, GCD_A_B, steps_sh, ms);//напечатать значение
			printf_s("%I64d ticks\n", __rdtsc() - start_tick);
			
			start_tick = __rdtsc();
			// Евклид
			beg_sh = chrono::steady_clock::now(); // Время перед поискои НОД
			unsigned long int steps_evkl = gcd_evklid(GCD_A_B, A, B); // НОД
			end_sh = chrono::steady_clock::now(); // Время после поиска НОД
			// Длительность работы в миллисекундах
			ms = (unsigned long int) (chrono::duration_cast<chrono::milliseconds>(end_sh - beg_sh)).count();
			
			gmp_printf("Evklid: gcd(%Zd,%Zd)=%Zd steps=%d %d ms ", A, B, GCD_A_B, steps_evkl, ms);//напечатать значение
			printf_s("%I64d ticks\n\n", __rdtsc() - start_tick);
		}
	}

	/*mpz_set_ui(A, 4);
	mpz_set_ui(B, 4);
	gcd(GCD_A_B, A,B);*/
	// Освобождение памяти
	mpz_clear(A);
	mpz_clear(B);
	mpz_clear(GCD_A_B);
	gmp_randclear(state);

	return 0;
}

