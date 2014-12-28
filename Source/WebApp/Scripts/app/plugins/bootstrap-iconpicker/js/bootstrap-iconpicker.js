/* ========================================================================
 * Bootstrap: bootstrap-iconpicker.js v1.0.0 by @recktoner
 * https://victor-valencia.github.com/bootstrap-iconpicker
 * ========================================================================
 * Copyright 2013 Victor Valencia Rico.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * ======================================================================== */


+function ($) { "use strict";


    // ICONPICKER PUBLIC CLASS DEFINITION
    // ==============================
    var Iconpicker = function (element, options) {
      this.$element = $(element);
      this.options  = $.extend({}, Iconpicker.DEFAULTS, this.$element.data());
      this.options  = $.extend({}, this.options, options);
    };

    Iconpicker.ICONSET = {
        glyphicon : [
            'adjust',
            'align-center',
            'align-justify',
            'align-left',
            'align-right',
            'arrow-down',
            'arrow-left',
            'arrow-right',
            'arrow-up',
            'asterisk',
            'backward',
            'ban-circle',
            'barcode',
            'bell',
            'bold',
            'book',
            'bookmark',
            'briefcase',
            'bullhorn',
            'calendar',
            'camera',
            'certificate',
            'check',
            'chevron-down',
            'chevron-left',
            'chevron-right',
            'chevron-up',
            'circle-arrow-down',
            'circle-arrow-left',
            'circle-arrow-right',
            'circle-arrow-up',
            'cloud',
            'cloud-download',
            'cloud-upload',
            'cog',
            'collapse-down',
            'collapse-up',
            'comment',
            'compressed',
            'copyright-mark',
            'credit-card',
            'cutlery',
            'dashboard',
            'download',
            'download-alt',
            'earphone',
            'edit',
            'eject',
            'envelope',
            'euro',
            'exclamation-sign',
            'expand',
            'export',
            'eye-close',
            'eye-open',
            'facetime-video',
            'fast-backward',
            'fast-forward',
            'file',
            'film',
            'filter',
            'fire',
            'flag',
            'flash',
            'floppy-disk',
            'floppy-open',
            'floppy-remove',
            'floppy-save',
            'floppy-saved',
            'folder-close',
            'folder-open',
            'font',
            'forward',
            'fullscreen',
            'gbp',
            'gift',
            'glass',
            'globe',
            'hand-down',
            'hand-left',
            'hand-right',
            'hand-up',
            'hd-video',
            'hdd',
            'header',
            'headphones',
            'heart',
            'heart-empty',
            'home',
            'import',
            'inbox',
            'indent-left',
            'indent-right',
            'info-sign',
            'italic',
            'leaf',
            'link',
            'list',
            'list-alt',
            'lock',
            'log-in',
            'log-out',
            'magnet',
            'map-marker',
            'minus',
            'minus-sign',
            'move',
            'music',
            'new-window',
            'off',
            'ok',
            'ok-circle',
            'ok-sign',
            'open',
            'paperclip',
            'pause',
            'pencil',
            'phone',
            'phone-alt',
            'picture',
            'plane',
            'play',
            'play-circle',
            'plus',
            'plus-sign',
            'print',
            'pushpin',
            'qrcode',
            'question-sign',
            'random',
            'record',
            'refresh',
            'registration-mark',
            'remove',
            'remove-circle',
            'remove-sign',
            'repeat',
            'resize-full',
            'resize-horizontal',
            'resize-small',
            'resize-vertical',
            'retweet',
            'road',
            'save',
            'saved',
            'screenshot',
            'sd-video',
            'search',
            'send',
            'share',
            'share-alt',
            'shopping-cart',
            'signal',
            'sort',
            'sort-by-alphabet',
            'sort-by-alphabet-alt',
            'sort-by-attributes',
            'sort-by-attributes-alt',
            'sort-by-order',
            'sort-by-order-alt',
            'sound-5-1',
            'sound-6-1',
            'sound-7-1',
            'sound-dolby',
            'sound-stereo',
            'star',
            'star-empty',
            'stats',
            'step-backward',
            'step-forward',
            'stop',
            'subtitles',
            'tag',
            'tags',
            'tasks',
            'text-height',
            'text-width',
            'th',
            'th-large',
            'th-list',
            'thumbs-down',
            'thumbs-up',
            'time',
            'tint',
            'tower',
            'transfer',
            'trash',
            'tree-conifer',
            'tree-deciduous',
            'unchecked',
            'upload',
            'usd',
            'user',
            'volume-down',
            'volume-off',
            'volume-up',
            'warning-sign',
            'wrench',
            'zoom-in',
            'zoom-out'
        ],
        fa : [
            'adjust',
            'anchor',
            'archive',
            'arrows',
            'arrows-h',
            'arrows-v',
            'automobile',
            'asterisk',
            'ban',
            'bank',
            'bar-chart-o',
            'barcode',
            'bars',
            'beer',
            'bell',
            'bell-o',
            'bolt',
            'bomb',
            'book',
            'bookmark',
            'bookmark-o',
            'briefcase',
            'bug',
            'building',
            'building-o',
            'bullhorn',
            'bullseye',
            'cab',
            'calendar',
            'calendar-o',
            'camera',
            'camera-retro',
            'car',
            'caret-square-o-down',
            'caret-square-o-left',
            'caret-square-o-right',
            'caret-square-o-up',
            'certificate',
            'check',
            'check-circle',
            'check-circle-o',
            'check-square',
            'check-square-o',
            'child',
            'circle',
            'circle-o',
            //'circle-notch',
            'circle-thin',
            'clock-o',
            'cloud',
            'cloud-download',
            'cloud-upload',
            'code',
            'code-fork',
            'coffee',
            'cog',
            'cogs',
            'comment',
            'comment-o',
            'comments',
            'comments-o',
            'compass',
            'credit-card',
            'crop',            
            'crosshairs',
            'cube',
            'cubes',
            'cutlery',
            'dashboard',
            'desktop',
            'dashboard',
            'database',
            'desktop',
            'dot-circle-o',
            'download',
            'edit',
            'ellipsis-h',
            'ellipsis-v',
            'envelope',
            'envelope-o',
            'envelope-square',
            'eraser',
            'exchange',
            'exclamation',
            'exclamation-circle',
            'exclamation-triangle',
            'external-link',
            'external-link-square',
            'eye',
            'eye-slash',
            'fax',
            'female',
            'fighter-jet',
            'file-archive-o',
            'file-audio-o',
            'file-code-o',
            'file-excel-o',
            'file-image-o',
            'file-movie-o',
            'file-pdf-o',
            'file-photo-o',
            'file-picture-o',
            'file-powerpoint-o',
            'file-sound-o',
            'file-video-o',
            'file-word-o',
            'file-zip-o',            
            'film',
            'filter',
            'fire',
            'fire-extinguisher',
            'flag',
            'flag-checkered',
            'flag-o',
            'flash',
            'flask',
            'folder',
            'folder-o',
            'folder-open',
            'folder-open-o',
            'frown-o',
            'gamepad',
            'gavel',
            'gear',
            'gears',
            'gift',
            'glass',
            'globe',
            'graduation-cap',
            'group',
            'hdd-o',
            'headphones',
            'heart',
            'heart-o',
            'history',
            'home',
            'image',
            'inbox',
            'info',
            'info-circle',
            'institution',
            'key',
            'keyboard-o',
            'language',
            'laptop',
            'leaf',
            'legal',
            'lemon-o',
            'level-down',
            'level-up',
            'life-bouy',
            'life-ring',
            'life-saver',
            'lightbulb-o',
            'location-arrow',
            'lock',
            'magic',
            'magnet',
            'mail-forward',
            'mail-reply',
            'mail-reply-all',
            'male',
            'map-marker',
            'meh-o',
            'microphone',
            'microphone-slash',
            'minus',
            'minus-circle',
            'minus-square',
            'minus-square-o',
            'mobile',
            'mobile-phone',
            'money',
            'moon-o',
            'mortar-board',
            'music',
            'navicon',
            'paper-plane',
            'paper-plane-o',
            'paw',
            'pencil',
            'pencil-square',
            'pencil-square-o',
            'phone',
            'phone-square',
            'photo',
            'picture-o',
            'plane',
            'plus',
            'plus-circle',
            'plus-square',
            'plus-square-o',
            'power-off',
            'print',
            'puzzle-piece',
            'qrcode',
            'question',
            'question-circle',
            'quote-left',
            'quote-right',
            'random',
            'refresh',
            'reorder',
            'reply',
            'reply-all',
            'retweet',
            'road',
            'rocket',
            'rss',
            'rss-square',
            'search',
            'search-minus',
            'search-plus',
            'send',
            'send-o',
            'share',
            'share-alt',
            'share-alt-square',
            'share-square',
            'share-square-o',
            'shield',
            'shopping-cart',
            'sign-in',
            'sign-out',
            'signal',
            'sitemap',
            'sliders',
            'smile-o',
            'sort',
            'sort-alpha-asc',
            'sort-alpha-desc',
            'sort-amount-asc',
            'sort-amount-desc',
            'sort-asc',
            'sort-desc',
            'sort-down',
            'sort-numeric-asc',
            'sort-numeric-desc',
            'sort-up',
            'space-shuttle',
            'spinner',
            'spoon',
            'square',
            'square-o',
            'star',
            'star-half',
            'star-half-empty',
            'star-half-full',
            'star-half-o',
            'star-o',            
            'suitcase',
            'sun-o',            
            'support',
            'tablet',
            'tachometer',
            'tag',
            'tags',
            'tasks',
            'taxi',
            'terminal',
            'thumb-tack',
            'thumbs-down',
            'thumbs-o-down',
            'thumbs-o-up',
            'thumbs-up',
            'ticket',
            'times',
            'times-circle',
            'times-circle-o',
            'tint',
            'toggle-down',
            'toggle-left',
            'toggle-right',
            'toggle-up',
            'trash-o',
            'tree',
            'trophy',
            'truck',
            'umbrella',
            'university',
            'unlock',
            'unlock-alt',
            'unsorted',
            'upload',
            'user',
            'users',
            'video-camera',
            'volume-down',
            'volume-off',
            'volume-up',
            'warning',
            'wheelchair',
            'wrench',                        
            'check-square',
            'check-square-o',
            'circle',
            'circle-o',
            'dot-circle-o',
            'minus-square',
            'minus-square-o',
            'plus-square',
            'plus-square-o',
            'square',
            'square-o',
            'bitcoin',
            'btc',
            'cny',
            'dollar',
            'eur',
            'euro',
            'gbp',
            'inr',
            'jpy',
            'krw',
            'money',
            'rmb',
            'rouble',
            'rub',
            'ruble',
            'rupee',
            'try',
            'turkish-lira',
            'usd',
            'won',
            'yen',
            'align-center',
            'align-justify',
            'align-left',
            'align-right',
            'bold',
            'chain',
            'chain-broken',
            'clipboard',
            'columns',
            'copy',
            'cut',
            'dedent',
            'eraser',
            'file',
            'file-o',
            'file-text',
            'file-text-o',
            'files-o',
            'floppy-o',
            'font',
            'header',
            'indent',
            'italic',
            'link',
            'list',
            'list-alt',
            'list-ol',
            'list-ul',
            'outdent',
            'paperclip',
            'paragraph',
            'paste',
            'repeat',
            'rotate-left',
            'rotate-right',
            'save',
            'scissors',
            'strikethrough',
            'subscript',
            'superscript',
            'table',
            'text-height',
            'text-width',
            'th',
            'th-large',
            'th-list',
            'underline',
            'undo',
            'unlink',
            'angle-double-down',
            'angle-double-left',
            'angle-double-right',
            'angle-double-up',
            'angle-down',
            'angle-left',
            'angle-right',
            'angle-up',
            'arrow-circle-down',
            'arrow-circle-left',
            'arrow-circle-o-down',
            'arrow-circle-o-left',
            'arrow-circle-o-right',
            'arrow-circle-o-up',
            'arrow-circle-right',
            'arrow-circle-up',
            'arrow-down',
            'arrow-left',
            'arrow-right',
            'arrow-up',
            'arrows',
            'arrows-alt',
            'arrows-h',
            'arrows-v',
            'caret-down',
            'caret-left',
            'caret-right',
            'caret-square-o-down',
            'caret-square-o-left',
            'caret-square-o-right',
            'caret-square-o-up',
            'caret-up',
            'chevron-circle-down',
            'chevron-circle-left',
            'chevron-circle-right',
            'chevron-circle-up',
            'chevron-down',
            'chevron-left',
            'chevron-right',
            'chevron-up',
            'hand-o-down',
            'hand-o-left',
            'hand-o-right',
            'hand-o-up',
            'long-arrow-down',
            'long-arrow-left',
            'long-arrow-right',
            'long-arrow-up',
            'toggle-down',
            'toggle-left',
            'toggle-right',
            'toggle-up',
            'arrows-alt',
            'backward',
            'compress',
            'eject',
            'expand',
            'fast-backward',
            'fast-forward',
            'forward',
            'pause',
            'play',
            'play-circle',
            'play-circle-o',
            'step-backward',
            'step-forward',
            'stop',
            'youtube-play',
            'adn',
            'android',
            'apple',
            'behance',
            'behance-square',
            'bitbucket',
            'bitbucket-square',
            'bitcoin',
            'btc',
            //'codeopen',
            'css3',
            'delicious',
            //'devianart',
            'digg',
            'dribbble',
            'dropbox',
            'drupal',
            'empire',
            'facebook',
            'facebook-square',
            'flickr',
            'foursquare',
            'ge',
            'git',
            'git-square',
            'github',
            'github-alt',
            'github-square',
            'gittip',
            'google',
            'google-plus',
            'google-plus-square',
            'hacker-news',
            'html5',
            'instagram',
            'joomla',
            'jsfiddle',
            'linkedin',
            'linkedin-square',
            'linux',
            'maxcdn',
            'openid',
            'pagelines',
            'pied-piper',
            'pied-piper-alt',
            'pied-piper-square',
            'pinterest',
            'pinterest-square',
            'qq',
            'ra',
            'rebel',
            'reddit',
            'reddit-square',
            'renren',
            'share-alt',
            'share-alt-square',
            'skype',
            'slack',
            'soundcloud',
            'spotify',
            'stack-exchange',
            'stack-overflow',
            'steam',
            'steam-square',
            'stumbleupon',
            'stumbleupon-circle',
            'tencent-weibo',
            'trello',
            'tumblr',
            'tumblr-square',
            'twitter',
            'twitter-square',
            'vimeo-square',
            'vine',
            'vk',
            'wechat',
            'weibo',
            'weixin',
            'windows',
            'wordpress',
            'xing',
            'xing-square',
            'yahoo',
            'youtube',
            'youtube-play',
            'youtube-square',
            'ambulance',
            'h-square',
            'hospital-o',
            'medkit',
            'plus-square',
            'stethoscope',
            'user-md',
            'wheelchair'
        ]
    };

    Iconpicker.DEFAULTS = {
        iconset: 'fontawesome',
        icon: '',
        rows: 4,
        cols: 4,
        placement: 'right',
    };

    Iconpicker.prototype.createButtonBar = function(){
        var op = this.options;
        var tr = $('<tr></tr>');
        for(var i = 0; i < op.cols; i++){
            var btn = $('<button class="btn btn-primary"><span class="glyphicon"></span></button>');
            var td = $('<td class="text-center"></td>');
            if(i == 0 || i == op.cols - 1){
                btn.val((i==0) ? -1: 1);
                btn.addClass((i==0) ? 'btn-previous': 'btn-next');
                btn.find('span').addClass( (i == 0) ? 'glyphicon-arrow-left': 'glyphicon-arrow-right');
                td.append(btn);
                tr.append(td);
            }
            else if(tr.find('.page-count').length == 0){
                td.attr('colspan', op.cols - 2).append('<span class="page-count"></span>');
                tr.append(td);
            }
        }
        op.table.find('thead').append(tr);
    };

    Iconpicker.prototype.updateButtonBar = function(page){
        var op = this.options;
        var total_pages = Math.ceil( op.icons.length / (op.cols * op.rows) );
        op.table.find('.page-count').html(page + ' / ' + total_pages);
        var btn_prev = op.table.find('.btn-previous');
        var btn_next = op.table.find('.btn-next');
        (page == 1) ? btn_prev.addClass('disabled'): btn_prev.removeClass('disabled');
        (page == total_pages) ? btn_next.addClass('disabled'): btn_next.removeClass('disabled');
    };

    Iconpicker.prototype.bindEvents = function(){
        var op = this.options;
        var el = this;
        op.table.find('.btn-previous, .btn-next').off('click').on('click', function(){
            var inc = parseInt($(this).val());
            el.changeList(op.page + inc);
        });
        op.table.find('.btn-icon').off('click').on('click', function(){
            el.select($(this).val());
            el.$element.popover('destroy');
        });
    };

    Iconpicker.prototype.select = function(icon){
        var op = this.options;
        var el = this.$element;
        op.selected = $.inArray(icon.replace(op.iconClassFix, ''), op.icons);
        if(op.selected == -1){
            op.selected = 0;
            icon = op.iconClassFix + op.icons[op.selected];
        }
        if(icon != '' && op.selected >= 0){
            op.icon = icon;
            el.find('input').val(icon);
            el.find('i').attr('class', '').addClass(op.iconClass).addClass(icon);
            el.trigger({ type: "change", icon: icon });
            op.table.find('button.btn-warning').removeClass('btn-warning');
        }
    };

    Iconpicker.prototype.switchPage = function(icon){
        var op = this.options;
        op.selected = $.inArray(icon.replace(op.iconClassFix, ''), op.icons);
        if(icon != '' && op.selected >= 0){
            var page = Math.ceil( (op.selected + 1) / (op.cols * op.rows) );
            this.changeList(page);
        }
        op.table.find('.'+icon).parent().addClass('btn-warning');
    };

    Iconpicker.prototype.changeList = function(page){
        var op = this.options;
        this.updateButtonBar(page);
        var tbody = op.table.find('tbody').empty();
        var offset = (page - 1) * op.rows * op.cols;
        for(var i = 0; i < op.rows; i++){
            var tr = $('<tr></tr>');
            for(var j = 0; j < op.cols; j++){
                var pos = offset + (i * op.cols) + j;
                var btn = $('<button class="btn btn-default btn-icon"></button>').hide();
                if(pos < op.icons.length){
                    var v = op.iconClassFix + op.icons[pos];
                    btn = $('<button class="btn btn-default btn-icon" value="' + v + '" title="' + v + '"><i class="' + op.iconClass + ' ' + v + '"></i></button>');
                }
                var td = $('<td></td>').append(btn);
                tr.append(td);
            }
            tbody.append(tr);
        }
        op.page = page;
        this.bindEvents();
    }

    Iconpicker.prototype.setIcon = function (icon) {
        this.select(icon);
    }

    // ICONPICKER PLUGIN DEFINITION
    // ========================
    var old = $.fn.iconpicker;
    $.fn.iconpicker = function (option, params) {
        return this.each(function () {
            var $this   = $(this);
            var data    = $this.data('bs.iconpicker');
            var options = typeof option == 'object' && option;
            if (!data) $this.data('bs.iconpicker', (data = new Iconpicker(this, options)));
            if (typeof option == 'string') data[option](params)
            else{
                var op = data.options;
                var ic = (op.iconset == 'fontawesome') ? 'fa' : 'glyphicon';
                op = $.extend(op, {
                    icons: Iconpicker.ICONSET[ic],
                    iconClass: ic,
                    iconClassFix: ic + '-',
                    page: 1,
                    selected: -1,
                    table: $('<table class="table-icons"><thead></thead><tbody></tbody></table>')
                });
                var name = ( typeof $this.attr('name') != 'undefined' ) ? 'name="' + $this.attr('name') + '"' : '';
                $this.empty()
                    .append('<i></i>')
                    .append('<input type="hidden" ' + name + '></input>')
                    .append('<span class="caret"></span>');
                $this.addClass('iconpicker');
                data.createButtonBar();
                data.changeList(1);
                $this.on('click', function(e){
                    e.preventDefault();
                    $this.popover({
                        animation: false,
                        trigger: 'manual',
                        html: true,
                        content: data.options.table,
                        container: 'body',
                        placement: data.options.placement
                    }).on('shown.bs.popover', function () {
                        data.switchPage(op.icon);
                        data.bindEvents();
                    });

                    $this.data('bs.popover').tip().addClass('iconpicker-popover')
                    $this.popover('show');
                });
                data.select(op.icon);
            }
        });
    };

    $.fn.iconpicker.Constructor = Iconpicker;


    // ICONPICKER NO CONFLICT
    // ==================
    $.fn.iconpicker.noConflict = function () {
        $.fn.iconpicker = old;
        return this;
    };


    // ICONPICKER DATA-API
    // ===============
    $('body').on('click', function (e) {
        $('.iconpicker').each(function () {
            //the 'is' for buttons that trigger popups
            //the 'has' for icons within a button that triggers a popup
            if ( ! $(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
                $(this).popover('destroy');
            }
        });
    });

}(window.jQuery);
