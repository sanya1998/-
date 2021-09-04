package ru.desyatovalexander10.daltonik;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.SeekBar;
import android.widget.TextView;
import android.widget.Toast;

public class MainActivity extends Activity implements View.OnClickListener, SeekBar.OnSeekBarChangeListener {
    private static final int requestCodeGame = 1;
    Button btnPlay, btnLeft, btnRight, btnExit, btnHints;
    TextView tvLevel, tvHints;
    SharedPreferences sPrefLevel, sPrefHint;
    final String SAVED_MAX_LEVEL = "MAX_LEVEL", SAVED_HINTS = "HINTS";
    int hints;
    int level_current;
    int level_max;
    SeekBar seekBarLevel;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        btnPlay = (Button) findViewById(R.id.btnPlay);
        btnPlay.setOnClickListener(this);

        btnHints = (Button) findViewById(R.id.btnHints);
        btnHints.setOnClickListener(this);

        btnExit = (Button) findViewById(R.id.btnExit);
        btnExit.setOnClickListener(this);

        btnLeft = (Button) findViewById(R.id.btnLeft);
        btnLeft.setOnClickListener(this);

        btnRight = (Button) findViewById(R.id.btnRight);
        btnRight.setOnClickListener(this);


        tvLevel = (TextView) findViewById(R.id.tvLevel);
        sPrefLevel = getPreferences(MODE_PRIVATE);
        level_max = sPrefLevel.getInt(SAVED_MAX_LEVEL, 0);
        tvLevel.setText(String.valueOf(level_max));
        level_current = level_max;

        seekBarLevel = (SeekBar) findViewById(R.id.seekBarLevel);
        seekBarLevel.setOnSeekBarChangeListener(this);
        seekBarLevel.setProgress(level_current);
        seekBarLevel.setMax(100);

        tvHints = (TextView) findViewById(R.id.tvHints);

        sPrefHint = getSharedPreferences(SAVED_HINTS, MODE_PRIVATE);
        hints = sPrefHint.getInt(SAVED_HINTS, 2);
        String str = String.valueOf(hints) +  " " + getString(R.string.hints);
        tvHints.setText(str);
    }

    @Override

    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.btnPlay:
                Intent intent = new Intent(this, Game.class);
                intent.putExtra("level", level_current);
                startActivityForResult(intent, requestCodeGame);
                break;
            case R.id.btnLeft:
                if(level_current > 0){
                    level_current--;
                }
                else{
                    level_current = level_max;
                }
                tvLevel.setText(String.valueOf(level_current));
                seekBarLevel.setProgress(level_current);
                break;
            case R.id.btnRight:
                if(level_current < level_max){
                    level_current++;
                }
                else{
                    level_current = 0;
                }
                tvLevel.setText(String.valueOf(level_current));
                seekBarLevel.setProgress(level_current);
                break;
            case R.id.btnHints:
                //Здесь показ рекламы

                hints += 5;
                SharedPreferences.Editor ed = sPrefHint.edit();
                ed.putInt(SAVED_HINTS, hints);
                ed.commit();
                String str = String.valueOf(hints) +  " " + getString(R.string.hints);
                tvHints.setText(str);
                break;
            case R.id.btnExit:
                finish();
                break;
            default:
                break;
        }
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if(resultCode == RESULT_OK) {
            if (level_current == level_max && level_max<100) {
                level_max++;
                SharedPreferences.Editor ed = sPrefLevel.edit();
                ed.putInt(SAVED_MAX_LEVEL,level_max);
                ed.commit();
                level_current++;
                if(level_current == 100){
                    Toast.makeText(this, "You won!", Toast.LENGTH_SHORT).show();
                }
            }
            else if(level_current<100){
                level_current++;
            }
            tvLevel.setText(String.valueOf(level_current));
        }
        hints = sPrefHint.getInt(SAVED_HINTS, 2);
        String str = String.valueOf(hints) +  " " + getString(R.string.hints);
        tvHints.setText(str);
        seekBarLevel.setProgress(level_current);
    }

    @Override
    public void onProgressChanged(SeekBar seekBar, int i, boolean b) {
        if(seekBarLevel.getProgress() > level_max) {
            level_current = level_max;
            seekBarLevel.setProgress(level_current);
        }
        else{
            level_current = seekBarLevel.getProgress();
            tvLevel.setText(String.valueOf(level_current));
        }
    }

    @Override
    public void onStartTrackingTouch(SeekBar seekBar) {

    }

    @Override
    public void onStopTrackingTouch(SeekBar seekBar) {

    }
}
