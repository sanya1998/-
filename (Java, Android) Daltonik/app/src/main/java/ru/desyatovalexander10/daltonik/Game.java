package ru.desyatovalexander10.daltonik;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.Gravity;
import android.view.View;
import android.view.animation.AnimationUtils;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.SeekBar;
import android.widget.TextView;
import android.widget.Toast;

import java.util.Random;

public class Game extends Activity implements View.OnClickListener, SeekBar.OnSeekBarChangeListener{
    LinearLayout GroupIV, llMainGame;
    final String SAVED_HINTS = "HINTS";
    SharedPreferences sPrefHint;
    Button btnHelp;
    TextView tvHints;
    int hints;
    int CountImageViews;
    ImageView[] imageViews;
    Random rnd;
    int answer, level;
    int success;
    int right_answers;
    int chance;
    SeekBar seekBarGame;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_game);

        Intent intent = getIntent();
        level = intent.getIntExtra("level",0);

        llMainGame = (LinearLayout) findViewById(R.id.llMainGame);
        GroupIV = (LinearLayout) findViewById(R.id.GroupIV);
        CountImageViews = GroupIV.getChildCount();

        imageViews = new ImageView[CountImageViews];
        for(int i=0; i<CountImageViews; i++) {
            imageViews[i] = (ImageView) GroupIV.getChildAt(i);
            imageViews[i].setOnClickListener(this);
        }
        rnd = new Random();

        right_answers = 0;

        tvHints = (TextView) findViewById(R.id.tvHints);

        btnHelp = (Button) findViewById(R.id.btnHelp);
        btnHelp.setOnClickListener(this);

        sPrefHint = getSharedPreferences(SAVED_HINTS, MODE_PRIVATE);
        hints = sPrefHint.getInt(SAVED_HINTS, 2);
        tvHints.setText("(" + String.valueOf(hints) + ")");

        success = 10 + level/5;

        chance = 100 - level;
        if(level < 33){
            chance /= 3;
        }
        else if(level < 66) {
            chance /= 2;
        }

        seekBarGame = (SeekBar) findViewById(R.id.seekBarGame);
        seekBarGame.setOnSeekBarChangeListener(this);
        seekBarGame.setProgress(right_answers);
        seekBarGame.setMax(success);

        NextTask();
    }

    public void onClick(View view) {
        if(view.equals(btnHelp)) {
            if(hints > 0) {
                imageViews[answer].startAnimation(AnimationUtils.loadAnimation(this, R.anim.reduce));
                GroupIV.setBackgroundColor(rnd.nextInt());
                hints--;
                tvHints.setText("(" + String.valueOf(hints) + ")");

                SharedPreferences.Editor ed = sPrefHint.edit();
                ed.putInt(SAVED_HINTS, hints);
                ed.commit();

            }
            return;
        }
        if(view.equals(imageViews[answer])) {
            right_answers++;
            seekBarGame.setProgress(right_answers);
            if(right_answers < success) {
                imageViews[answer].startAnimation(AnimationUtils.loadAnimation(this, R.anim.zoom));
                NextTask();
            }
            else{

                Toast toast = Toast.makeText(this, "Success!", Toast.LENGTH_SHORT);
                LinearLayout toastImage = (LinearLayout) toast.getView();
                ImageView imageView = new ImageView(Game.this);
                imageView.setImageResource(R.mipmap.smile);
                toastImage.addView(imageView,1);
                toast.setGravity(Gravity.CENTER_HORIZONTAL, 0,0);
                toast.setGravity(Gravity.TOP, 0,0);
                toast.show();

                setResult(RESULT_OK);
                finish();
            }
        }
        else{
            Toast toast = Toast.makeText(this, "Game over!", Toast.LENGTH_SHORT);
            LinearLayout toastImage = (LinearLayout) toast.getView();
            ImageView imageView = new ImageView(Game.this);
            imageView.setImageResource(R.mipmap.no_smile);
            toastImage.addView(imageView,1);
            toast.setGravity(Gravity.CENTER_HORIZONTAL, 0,0);
            toast.setGravity(Gravity.TOP, 0,0);
            toast.show();

            setResult(RESULT_CANCELED);
            finish();
        }
    }

    private void NextTask() {
        llMainGame.setBackgroundColor(rnd.nextInt());

        int rnd_red = rnd.nextInt(255);
        int rnd_green = rnd.nextInt(255);
        int rnd_blue = rnd.nextInt(255);
        int color_int = (255 << 24) |
                (rnd_red << 16) |
                (rnd_green << 8) |
                (rnd_blue);

        rnd_red = rnd_red + chance > 255 ? rnd_red - chance : rnd_red + chance;
        if(level<66) {
            chance += level%2;
            rnd_green = rnd_green + chance > 255 ? rnd_green - chance : rnd_green + chance;
        }
        if(level<33) {
            if(level%6==0)      chance +=1;
            else if(level%5==0) chance -= 2;
            else if(level%4==0) chance += 2;
            else if(level%3==0) chance -= 1;
            rnd_blue = rnd_blue + chance > 255 ? rnd_blue - chance : rnd_blue + chance;
        }
        int color_int_different = (255 << 24) |
                (rnd_red << 16) |
                (rnd_green << 8) |
                (rnd_blue);
        for(int i=0; i<CountImageViews; i++) {
            imageViews[i].setBackgroundColor(color_int);
        }
        answer = rnd.nextInt(CountImageViews);
        imageViews[answer].setBackgroundColor(color_int_different);
    }

    @Override
    public void onProgressChanged(SeekBar seekBar, int i, boolean b) {
        seekBarGame.setProgress(right_answers);
    }

    @Override
    public void onStartTrackingTouch(SeekBar seekBar) {

    }

    @Override
    public void onStopTrackingTouch(SeekBar seekBar) {

    }
}
